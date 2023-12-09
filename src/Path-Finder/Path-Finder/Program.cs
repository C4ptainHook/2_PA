
namespace Path_Finder
{
    using System;
    using System.Diagnostics;
    using System.Drawing.Printing;
    using CommandLine;
    using Path_Finder.Algorithms;
    using Path_Finder.ArgsSettings;
    using Path_Finder.MazeDomain;

    public partial class Program
    {
        private static readonly Stopwatch _stopwatch = new Stopwatch();
        [STAThread]
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CLOptions>(args)
                .WithParsed<CLOptions>((options) =>
                {
                    //options.LDFS = true;
                    //options.LDFSDepth = 100; //debug

                    options.RBFS = true;//debug

                    try
                    {
                        ValidateOptions(options);
                    }
                    catch (ArgumentNullException NullEx)
                    { Console.WriteLine(NullEx.Message); }
                    catch (ArgumentOutOfRangeException outOfRangeEx)
                    { Console.WriteLine(outOfRangeEx.Message); }

                    Maze mazeForSolving;
                    SearchDetails progress;

                    if (options.MSize != 0)
                    {
                        mazeForSolving = MazeGenerator.InitialiseMaze(options.MSize);
                    }
                    else
                    {
                        mazeForSolving = MazeDialog.LoadMaze();
                    }

                    while (options.LDFS || options.RBFS)
                    {
                        if (options.LDFS)
                        {
                            var ldfs = new LDFS(mazeForSolving, options.LDFSDepth);
                            progress = ldfs.GetPathTick();
                            while (progress.PathPossible && !progress.PathFound)
                            {
                                progress = ldfs.GetPathTick();
                            }
                            options.LDFS = false;
                        }
                        else
                        {
                            _stopwatch.Reset();
                            _stopwatch.Start();
                            var rbfs = new RBFS(mazeForSolving);
                            progress = rbfs.GetPathTick();
                            options.RBFS = false;
                            Console.WriteLine($"Elapsed time {_stopwatch.Elapsed.TotalSeconds}");
                        }

                        if (progress.Path != null)
                        {
                            Console.WriteLine("Solution was found\nPath:[");
                            foreach (var coord in progress.Path)
                            {
                                Console.Write(coord.ToString() + ",");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Solution was`nt found");
                        }
                    }
                });
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

            if (!options.LDFS && !options.RBFS)
            {
                throw new ArgumentNullException("Oops...At least one algorithm has to be used");
            }
        }
    }
}