using Path_Finder.Algorithms;
using Path_Finder.MazeDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Path_Finder.Tests
{
    public class RBFSTests
    {
        [Fact]
        public void RBFS_PathFound() 
        {
            var maze = MazeDialog.LoadMaze(@"Assets\RBFSTestMaze.maze");
            var rbfs = new RBFS(maze);
            Coord[] expected =
            {
            new Coord(0,0), new Coord(1,0), new Coord(1,1), new Coord(1,2),
            new Coord(1,3), new Coord(1,4), new Coord(1,5), new Coord(1,6),
            new Coord(2,6), new Coord(3,6), new Coord(3,7), new Coord(4,7),
            new Coord(5,7), new Coord(5,8), new Coord(5,9), new Coord(6,9),
            new Coord(7,9), new Coord(8,9), new Coord(9,9)
            };

            var actual = rbfs.GetPathTick();
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
