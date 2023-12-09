
namespace Path_Finder.MazeDomain
{
    using Path_Finder.Algorithms;

    public static class MazeGenerator
    {
        public static Maze InitialiseMaze(int size)
        {
            var randommaze = new Maze(size, size);
            var workingseed = 0;
            while (workingseed == 0)
            {
                Console.WriteLine("Generating maze..\n");
                workingseed = findWorkingSeed(randommaze);
            }
            return randommaze;
        }
        private static int findWorkingSeed(Maze randomMaze)
        {
            throw new NotImplementedException("Maze gen not implemented");
            randomMaze.Randomize();
            LDFS testpathfinder = null;
            var progress = testpathfinder.GetPathTick();
            while (progress.PathPossible && !progress.PathFound)
            {
                progress = testpathfinder.GetPathTick();
            }

            return progress.PathFound ? 1 : 0;
        }
    }
}
