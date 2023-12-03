
namespace Path_Finder.Algorithms
{
    using Path_Finder.MazeDomain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public abstract class AlgorithmBase
    {
        protected readonly Maze Maze;
        protected readonly List<Node> Closed;
        protected List<Coord> Path;
        protected readonly Coord Origin;
        protected readonly Coord Destination;
        protected int Id;
        protected Node CurrentNode;
        protected int Operations;
        public string AlgorithmName;


        protected AlgorithmBase(Maze maze)
        {
            Maze = maze;
            Closed = new List<Node>();
            Origin = Maze.GetStart().Coord;
            Destination = Maze.GetEnd().Coord;
            Operations = 0;
            Id = 1;
        }

        public abstract SearchDetails GetPathTick();

        /// <summary>
        /// Find the coords that are above, below, left, and right of the current cell, assuming they are valid
        /// </summary>
        /// <param name="current"></param>
        /// <returns>The valid coords around the current cell</returns>
        protected virtual IEnumerable<Coord> GetNeighbours(Node current)
        {
            var neighbours = new List<Cell>
        {
            Maze.GetCell(current.Coord.X - 1, current.Coord.Y),
            Maze.GetCell(current.Coord.X + 1, current.Coord.Y),
            Maze.GetCell(current.Coord.X, current.Coord.Y - 1),
            Maze.GetCell(current.Coord.X, current.Coord.Y + 1)
        };

            return neighbours.Where(x => x.Type != Enums.CellType.Invalid && x.Type != Enums.CellType.Solid).Select(x => x.Coord).ToArray();
        }

        protected abstract SearchDetails GetSearchDetails();

        protected static bool CoordsMatch(Coord a, Coord b) => a.X == b.X && a.Y == b.Y;

        /// <summary>
        /// Get the total blocks horizontally and vertically from one coord to another
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns>Distance in blocks</returns>
        protected static double GetEuclidDistance(Coord origin, Coord destination)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(origin.X - destination.X), 2) + Math.Pow(Math.Abs(origin.Y - destination.Y), 2));
        }

        /// <summary>
        /// Get the cost of the path between A and B
        /// </summary>
        /// <returns>Cost of the path or 0 if no path has been found</returns>
        protected int GetPathCost()
        {
            if (Path == null) return 0;

            var cost = 0;
            foreach (var step in Path)
                cost += Maze.GetCell(step.X, step.Y).Weight;

            return cost;
        }
    }
}
