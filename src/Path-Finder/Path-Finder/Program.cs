
namespace Path_Finder
{
    using System;
    using System.Diagnostics;
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
                    try
                    {
                        ValidateOptions(options);
                    }
                    catch (ArgumentNullException NullEx)
                    { Console.WriteLine(NullEx.Message); }

                    Maze mazeForSolving;
                    SearchDetails progress;

                    while (options.LDFS || options.RBFS)
                    {
                        if (options.LDFS && options.LDFSDepth != 0)
                        {
                            _stopwatch.Reset();
                            _stopwatch.Start();
                            mazeForSolving = MazeDialog.LoadMaze();
                            var ldfs = new LDFS(mazeForSolving, options.LDFSDepth);
                            Console.WriteLine($"Algorithm: {ldfs.AlgorithmName}");
                            progress = ldfs.GetPathTick();
                            while (progress.PathPossible && !progress.PathFound)
                            {
                                progress = ldfs.GetPathTick();
                            }
                            options.LDFS = false;
                        }
                        else 
                        {
                            mazeForSolving = MazeDialog.LoadMaze();
                            _stopwatch.Reset();
                            _stopwatch.Start();
                            var rbfs = new RBFS(mazeForSolving);
                            Console.WriteLine($"Algorithm: {rbfs.AlgorithmName}");
                            progress = rbfs.GetPathTick();
                            options.RBFS = false;
                        }

                        if (progress.Path != null)
                        {
                            Console.Write("Solution was found\nPath:[");
                            foreach (var coord in progress.Path)
                            {
                                Console.Write(coord.ToString() + ' ');
                            }
                            Console.WriteLine(']');
                            Console.WriteLine($"Elapsed time: {_stopwatch.Elapsed.TotalSeconds} seconds" + '\n');
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
            if (!options.LDFS && !options.RBFS)
            {
                throw new ArgumentNullException("Oops...At least one algorithm has to be used");
            }
        }
    }
}