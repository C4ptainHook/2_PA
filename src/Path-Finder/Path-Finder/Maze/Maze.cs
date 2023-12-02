using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path_Finder.Maze
{
    using static Enums;

    public class Maze
    {
        private readonly Cell[,] _maze;
        public Maze(int horizontalCells, int verticalCells)
        {
            _maze = new Cell[horizontalCells, verticalCells];
            for (var x = 0; x < _maze.GetLength(0); x++)
            {
                for (var y = 0; y < _maze.GetLength(1); y++)
                {
                    SetCell(x, y, CellType.Empty);
                }
            }

            SetStartAndEnd();
        }

        public void Randomize()
        {
            Randomize((int)DateTime.Now.Ticks);
        }

        public void Randomize(int seed)
        {
            var rand = new Random(seed);

            // Iterate through the whole grid
            for (var x = 0; x < _maze.GetLength(0); x++)
            {
                for (var y = 0; y < _maze.GetLength(1); y++)
                {
                    // Make each cell either solid or empty at random
                    _maze[x, y].Type = rand.Next(0, 10) > 5 ? CellType.Solid : CellType.Empty;
                    if (_maze[x, y].Type != CellType.Empty) continue;

                    // If it's empty, randomly give the path a weight
                    var weightSpread = rand.Next(0, 10);
                    if (weightSpread > 8)
                        _maze[x, y].Weight = 3;
                    else if (weightSpread > 6)
                        _maze[x, y].Weight = 2;
                    else
                        _maze[x, y].Weight = 1;
                }
            }
            SetStartAndEnd();
        }

        public Cell GetCell(int x, int y)
        {
            if (x > _maze.GetLength(0) - 1 || x < 0 || y > _maze.GetLength(1) - 1 || y < 0) return new Cell { Coord = new Coord(-1, -1), Type = CellType.Invalid };

            return _maze[x, y];
        }

        public Cell GetStart()
        {
            return _maze.Cast<Cell>().FirstOrDefault(cell => cell.Type == CellType.A);
        }

        public Cell GetEnd()
        {
            return _maze.Cast<Cell>().FirstOrDefault(cell => cell.Type == CellType.B);
        }

        public void SetCell(int x, int y, CellType type)
        {
            _maze[x, y] = new Cell
            {
                Coord = new Coord(x, y),
                Type = type,
                Weight = GetCell(x, y)?.Weight ?? 0
            };

            SetStartAndEnd();
        }

        public void SetCell(Coord coord, CellType type)
        {
            SetCell(coord.X, coord.Y, type);
        }

        public int GetCountOfType(CellType type)
        {
            var total = 0;
            foreach (var cell in _maze)
            {
                total += cell.Type == type ? 1 : 0;
            }

            return total;
        }

        public int GetTraversableCells()
        {
            return GetCountOfType(CellType.Open) + GetCountOfType(CellType.A) + GetCountOfType(CellType.B);
        }

        private void SetStartAndEnd()
        {
            _maze[0, 0] = new Cell
            {
                Coord = new Coord(0, 0),
                Type = CellType.A
            };
            _maze[_maze.GetLength(0) - 1, _maze.GetLength(1) - 1] = new Cell
            {
                Coord = new Coord(_maze.GetLength(0) - 1, _maze.GetLength(1) - 1),
                Type = CellType.B
            };
        }
    }
}
