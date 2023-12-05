namespace Path_Finder.ArgsSettings
{
    using System;
    using CommandLine;

    public class CLOptions
    {
        public readonly int MaxMazeSize = 10000;

        [Option('m', "maze", Required = false, HelpText = "Defines size of maze to generate")]
        public int MSize { get; set; } = 0;

        [Option("LDFS", Required = false, HelpText = "Utilize LDFS algorithm to solve the problem")]
        public bool LDFS { get; set; } = false;

        [Option('d', "depth", Required = false, HelpText = "Limits depth of recursion to the provided value")]
        public uint LDFSDepth { get; set; } = 0;

        [Option("RBFS", Required = false, HelpText = "Utilize RBFS algorithm to solve the problem")]
        public bool RBFS { get; set; } = false;
    }
}
