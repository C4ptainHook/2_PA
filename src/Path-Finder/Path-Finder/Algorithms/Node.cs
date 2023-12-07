
namespace Path_Finder.Algorithms
{
    using Path_Finder.MazeDomain;
    public class Node
    {
        public Node(int id, int? parentId, int x, int y, double g, double h)
        {
            Id = id;
            ParentId = parentId;
            Coord = new Coord(x, y);
            G = g;
            H = h;
            F = G + H;
        }

        public Node(int id, int? parentId, Coord coord, double g, double h)
        {
            Id = id;
            ParentId = parentId;
            Coord = coord;
            G = g;
            H = h;
            F = G + H;
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public Coord Coord { get; set; }
        public double F { get; set; }
        public double G { get; set; }
        public double H { get; set; }
    }
}
