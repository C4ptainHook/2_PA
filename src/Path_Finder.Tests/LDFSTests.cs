namespace Path_Finder.Tests
{
    using Path_Finder;
    using Path_Finder.Algorithms;
    using Path_Finder.MazeDomain;
    using System;
    using Xunit;

    public class LDFSTests
    {
        [Fact]
        public void LDFS_PathFound() 
        {
            var maze = MazeDialog.LoadMaze(@"Assets\LDFSTestMaze.maze");
            var ldfs = new LDFS(maze, 100);
            Coord[] expected =
            {
            new Coord(0,0), new Coord(1,0), new Coord(1,1), new Coord(1,2),
            new Coord(2,2), new Coord(3,2), new Coord(4,2), new Coord(4,3),
            new Coord(5,3), new Coord(5,4), new Coord(5,5)
            };
            var actual = ldfs.GetPathTick();

            while (actual.PathPossible && !actual.PathFound)
            {
                actual = ldfs.GetPathTick();
            }
            bool IsMatch = false;
            var predicate = (Coord a, Coord b) =>
            {
                return a.X == b.X && a.Y == b.Y;
            };
            for (var i = 0; i < expected.Length; i++)
            {
                IsMatch = predicate(expected[i], actual.Path[i]);
            }
            Assert.True(IsMatch);
        }
    }
}