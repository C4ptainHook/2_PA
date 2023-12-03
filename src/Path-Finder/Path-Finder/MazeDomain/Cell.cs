using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Path_Finder.MazeDomain.Enums;

namespace Path_Finder.MazeDomain
{
   

    public class Cell
    {
        public Coord Coord { get; set; }
        public CellType Type { get; set; }
        public int Weight { get; set; }
    }
    
}
