
using Microsoft.VisualBasic.Logging;
using System.IO.Packaging;
using System.Windows.Forms;
using static Path_Finder.MazeDomain.Enums;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Path_Finder.MazeDomain
{
    public static class MazeDialog
    {
        private delegate void SemiAction(string[] seq);
        private static string SaveDialog() 
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Maze Files (*.maze)|*.maze";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                    return saveDialog.FileName;
                else
                {
                    throw new NullReferenceException("Save file is NULL");
                }
            }
        }
        private static string OpenDialog()
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Maze Files (*.maze)|*.maze";
                if (openDialog.ShowDialog() == DialogResult.OK)
                    return openDialog.FileName;
                else
                    throw new NullReferenceException("Open file is NULL");
            }
        }
        public static void SaveMaze(Maze targetMaze)
        {
            try
            {
                string? fileName = SaveDialog();
                using (var writer = new StreamWriter(File.OpenWrite(fileName)))
                {
                    string size = targetMaze.Rows.ToString() + 'x' + targetMaze.Columns.ToString();
                    writer.WriteLine(size);
                    int flow = 0;
                    foreach(Cell cell in targetMaze)
                    {
                        if(flow !=0)
                        {
                            if(flow % targetMaze.Columns == 0)
                                writer.Write('\n');
                            else
                                writer.Write(' ');
                        }
                        switch (cell.Type)
                        {
                            case(CellType.Empty): { writer.Write(0); break; }
                            case(CellType.Solid): { writer.Write(1); break; }
                            default: { writer.Write(0); break; }
                        }
                        flow++;
                    }
                }
            }
            catch (NullReferenceException nullEx) 
            {
                Console.WriteLine(nullEx.Message);
            }
        }
        public static Maze LoadMaze()
        {
            try
            {
                string? fileName = OpenDialog();
                Maze targetMaze;
                using (var reader = new StreamReader(File.OpenRead(fileName)))
                {
                    string line = reader.ReadLine().Trim(' ', '\n');
                    string[] parts = line.Split('x');

                    if (parts.Length == 2 && int.TryParse(parts[0], out int Rows) && int.TryParse(parts[1], out int Columns))
                    {
                       targetMaze = new Maze(Rows, Columns);
                    }
                    else
                    {
                        throw new Exception("Incorrect size format");
                    }

                    var filler = TranslateMaze(targetMaze);
                    while(reader.Peek() >= 0)
                    {
                        filler(reader.ReadLine().Split(' '));
                    }
                    return targetMaze;
                }
            }
            catch (NullReferenceException nullEx)
            {
                Console.WriteLine(nullEx.Message);
                return null;
            }
        }
        private static SemiAction TranslateMaze(Maze maze)
        {
            int i = 0;

           return (string[] sequence) =>
            {
                for (int j = 0; j < sequence.Length; j++)
                {
                    int.TryParse(sequence[j], out int number);
                    switch (number)
                    {
                        case (0):
                            {
                                maze.SetCell(i,j, CellType.Empty); break;
                            }
                        case (1):
                            {
                                maze.SetCell(i, j, CellType.Solid); break;
                            }
                        default:
                            {
                                throw new Exception("Wrong value in maze set");
                            }
                    }
                 
                }
                i++;
            };
        }
    }
}
