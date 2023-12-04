
namespace Path_Finder
{
    using System;
    using System.Drawing.Printing;
    using CommandLine;
    using Path_Finder.Algorithms;
    using Path_Finder.ArgsSettings;
    using Path_Finder.MazeDomain;
    using static System.Net.Mime.MediaTypeNames;

    public partial class Program
    {
        private readonly System.Timers.Timer _pathTimer;

        [STAThread]
        public static void Main(string[] args)
        {
            var Maze = MazeDialog.LoadMaze();
            MazeDialog.SaveMaze(Maze);
            //Parser.Default.ParseArguments<CLOptions>(args)
            //    .WithParsed<CLOptions>((options) => 
            //    {
            //        try
            //        {
            //            ValidateOptions(options);
            //        }
            //        catch(ArgumentNullException NullEx) 
            //        { Console.WriteLine(NullEx.Message); }
            //        catch(ArgumentOutOfRangeException outOfRangeEx)
            //        { Console.WriteLine(outOfRangeEx.Message); }

            //        Maze mazeForSolving;

            //        if (options.MSize != 0)
            //        {
            //            mazeForSolving = MazeGenerator.InitialiseMaze(options.MSize);
            //        }
            //        else
            //        {
            //            mazeForSolving = MazeDialog.LoadMaze(); 
            //        }

            //        while (options.LDFS || options.RBFS) 
            //        {
                            
            //        }
            //    });
        }


        private static void ValidateOptions(CLOptions options)
        {
            if (options.LDFS && options.LDFSDepth == 0)
            {
                throw new ArgumentNullException("Depth parameter for LDFS wasnt specified");
            }

            if (options.MSize < 0 || options.MSize > options.MaxMazeSize)
            {
                throw new ArgumentOutOfRangeException($"Maze size has size out of range.\n[Current size:{options.MSize} - 0 < Allowed size < {options.MaxMazeSize}");
            }
        }
    }
}