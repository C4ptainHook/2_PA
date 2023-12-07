
using Path_Finder.MazeDomain;

namespace Path_Finder.Algorithms
{
    class RBFS: AlgorithmBase
    {

        readonly Stack<Node> _stack = new Stack<Node>();
        public uint fLim { get; private set; }

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
            return RBFSSearch(_stack.Peek(), int.MaxValue);
        }   public Tuple<Node,Node> GetBestAndAlternative(Coord[] succesors)
        {
            var nodifiedNeigbors = new List<Node>();
            Tuple<Node,Node> firstAndSecond;
            foreach (var coord in succesors)
            {
                nodifiedNeigbors.Add(new Node(Id++, null, coord.X, coord.Y, _stack.Count, GetEuclidDistance(coord, Destination)));
            }
            var orderedNeighbors = nodifiedNeigbors.OrderBy(x => x.F).ToList();
            if (nodifiedNeigbors.Count == 1)
                firstAndSecond = new(orderedNeighbors[0], new Node(0, 0, null, 0, double.MaxValue));
            else
            {
                firstAndSecond = new(orderedNeighbors[0], orderedNeighbors[1]);
            }
            return firstAndSecond;
        }
     
        public SearchDetails RBFSSearch(Node node, double fLimit)
        {
            SearchDetails ans = new SearchDetails();
            CurrentNode = _stack.Peek();
            if (CoordsMatch(CurrentNode.Coord, Destination))
            {
                // All the items on the stack will be the path so add them and reverse the order
                Path = new List<Coord>();
                foreach (var item in _stack)
                    Path.Add(item.Coord);

                Path.Reverse();

                return GetSearchDetails();
            }

            var _neighbours = GetNeighbours(CurrentNode).Where(x => !AlreadyVisited(new Coord(x.X, x.Y))).ToArray();

            if (_neighbours.Any())
            {
                Node best;
                double alternative;
                while (true)
                {
                    var bATuple = GetBestAndAlternative(_neighbours);
                    best = bATuple.Item1;
                    alternative = bATuple.Item2.F;
                    if (bATuple.Item1.F > fLimit) { _stack.Pop(); ans.FLimit = best.F; ans.Path = null; return ans;}
                    _stack.Push(best);
                    SearchDetails result = RBFSSearch(best, Math.Min(fLimit, alternative));
                    best.F = result.FLimit;
                    if (result.Path != null) return result;
                }
            }
            else
            {
                // Remove this unused node from the stack
                _stack.Pop();
                ans.FLimit = double.MaxValue; ans.Path = null;
                return ans;
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
