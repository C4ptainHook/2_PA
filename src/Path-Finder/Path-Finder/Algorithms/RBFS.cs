
using Path_Finder.MazeDomain;

namespace Path_Finder.Algorithms
{
    class RBFS: AlgorithmBase
    {

        readonly Stack<Node> _stack = new Stack<Node>();

        public RBFS(Maze maze): base(maze)
        {
            AlgorithmName = "RBFS";
            _stack.Push(new Node(Id++, null, Origin, 0, GetEuclidDistance(Origin, Destination)));
        }

        private static double GetH(Coord origin, Coord destination)
        {
            return GetEuclidDistance(origin, destination);
        }
        private bool AlreadyVisited(Coord coord)
        {
            return _stack.Any(x => CoordsMatch(x.Coord, coord));
        }
        public override SearchDetails GetPathTick()
        {
            return RBFSSearch(double.MaxValue);
        } 
        public Tuple<Node,Node> GetBestAndAlternative(List<Node> succesors)
        {
            Tuple<Node, Node> firstAndSecond;
            var orderedNeighbors = succesors.OrderBy(x => x.F).ToList();
            if (orderedNeighbors.Count == 1)
                firstAndSecond = new(orderedNeighbors[0], new Node(0, 0, null, 0, double.MaxValue));
            else
            {
                firstAndSecond = new(orderedNeighbors[0], orderedNeighbors[1]);
            }
            return firstAndSecond;
        }

        public List<Node> NodifyNeigbors(Coord[] succesors)
        {
            var nodifiedNeigbors = new List<Node>();
            foreach (var coord in succesors)
            {
                nodifiedNeigbors.Add(new Node(Id++, null, coord.X, coord.Y, _stack.Count, GetEuclidDistance(coord, Destination)));
            }
            return nodifiedNeigbors;
        }

        public SearchDetails RBFSSearch(double FLimit)
        {
            SearchDetails answer;
            CurrentNode = _stack.Peek();
            Console.WriteLine($"Current node{CurrentNode.Coord}");//debug
            if (CoordsMatch(CurrentNode.Coord, Destination))
            {
                Console.WriteLine($"SOLUTION FOUND"); //debug
                // All the items on the stack will be the path so add them and reverse the order
                Path = new List<Coord>();
                foreach (var item in _stack)
                    Path.Add(item.Coord);
                Path.Reverse();
                answer = GetSearchDetails();
                answer.FLimit = 0;
                return answer;
            }


            var _neighbours = NodifyNeigbors(GetNeighbours(CurrentNode).Where(x => !AlreadyVisited(new Coord(x.X, x.Y))).ToArray());
            

            for(var i = 0; i < _neighbours.Count; i++ )
            {
                Console.WriteLine($"Neigbor {i} = {_neighbours[i]}"); //debug
            }

            if (_neighbours.Any())
            {
                Node best;
                double alternative;
                while (true)
                {
                    var bATuple = GetBestAndAlternative(_neighbours);
                    best = bATuple.Item1;
                    alternative = bATuple.Item2.F;
                    Console.WriteLine($"Best = {best.Coord}");//debug
                    if (bATuple.Item1.F > FLimit) 
                    {
                        Console.WriteLine($"{_stack.Peek().Coord} видалено зі стеку");//debug
                        _stack.Pop();
                        CurrentNode = _stack.Peek();
                        answer = GetSearchDetails();
                        answer.FLimit = best.F; 
                        return answer;
                    }
                    _stack.Push(best);
                    Console.WriteLine($"Best = {best.Coord} додано у стек");//debug
                    SearchDetails result = RBFSSearch(Math.Min(FLimit, alternative));
                    best.F = result.FLimit;
                    if (result.Path != null) return result;
                }
            }
            else
            {
                // Remove this unused node from the stack
                Console.WriteLine($"{_stack.Peek().Coord} видалено зі стеку");//debug
                _stack.Pop();
                answer = GetSearchDetails();
                answer.FLimit = double.MaxValue; 
                return answer;
            }
        }
        protected override SearchDetails GetSearchDetails()
        {
            return new SearchDetails
            {
                Path = Path?.ToArray(),
                PathCost = GetPathCost(),
                LastNode = CurrentNode,
                DistanceOfCurrentNode = CurrentNode == null ? 0 : GetH(CurrentNode.Coord, Destination),
                ClosedListSize = Closed.Count,
                UnexploredListSize = Maze.GetCountOfType(Enums.CellType.Empty),
                Operations = Operations++
            };
        }

    }
}
