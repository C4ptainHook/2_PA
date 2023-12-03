
namespace Path_Finder
{
    using System;
    using System.Linq;
    using Path_Finder.Algorithms;
    using Path_Finder.MazeDomain;
    using static System.Net.Mime.MediaTypeNames;

    public partial class Program
    {
        private AlgorithmBase[] _algorithms;
        private int _currentAlgorithm;
        private readonly System.Timers.Timer _pathTimer;
        private const int Delay = 5;

        [STAThread]
        public static void Main()
        {
            var maze = MazeDialog.LoadMaze();
            MazeDialog.SaveMaze(maze);
        }

        /// <summary>
        /// Set up a maze and initialise the algorithm variables
        /// </summary>
        private void InitialiseMaze()
        {
            _pathTimer.Stop();

            // Generate mazes until one if made that has a valid path between A and B
            //var workingSeed = 0;
            //while (workingSeed == 0)
                //workingSeed = FindWorkingSeed();
        }

        /// <summary>
        /// Generate mazes until one is found that has a valid path between A and B
        /// </summary>
        /// <returns>The seed of the valid maze</returns>
        //private int FindWorkingSeed()
        //{
        //    var testPathFinder = new DFS(testMazeDrawer.Grid);
        //    var progress = testPathFinder.GetPathTick();
        //    while (progress.PathPossible && !progress.PathFound)
        //    {
        //        progress = testPathFinder.GetPathTick();
        //    }

        //    return progress.PathFound ? testMazeDrawer.Seed : 0;
        //}
    }
}