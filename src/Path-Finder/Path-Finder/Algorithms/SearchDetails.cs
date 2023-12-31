﻿
namespace Path_Finder.Algorithms
{
    using Path_Finder.MazeDomain;

    public class SearchDetails
    {
        public bool PathPossible => PathFound || OpenListSize > 0;
        public bool PathFound => Path != null;
        public Coord[] Path { get; set; }
        public int PathCost { get; set; }
        public Node LastNode { get; set; }
        public double DistanceOfCurrentNode { get; set; }
        public int OpenListSize { get; set; }
        public int ClosedListSize { get; set; }
        public int UnexploredListSize { get; set; }
        public int Operations { get; set; }
        public double FLimit { get; set; }
    }
}
