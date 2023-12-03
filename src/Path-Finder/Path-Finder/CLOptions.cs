namespace Path_Finder.ArgsSettings
{
    using System;
    using CommandLine;

    public class CLOptions
    {
        [Option('m', "maze", Required = false, HelpText = "Defines size of maze to generate")]
        public int MSize { get; set; } = 10;

        [Option("LDFS", Required = false, HelpText = "Utilize LDFS algorithm to solve the problem")]
        public bool LDFS { get; set; } = false;

        [Option('d', "depth", Required = false, HelpText = "Limits depth of recursion to the provided value")]
        public int LDFSDepth { get; set; } = 0;

        [Option("RBFS", Required = false, HelpText = "Utilize RBFS algorithm to solve the problem")]
        public bool RBFS { get; set; } = false;

        public void ValidateLDFSOption() 
        {
            if (LDFS && LDFSDepth == 0)
            {
                throw new ArgumentNullException("Depth parameter for LDFS wasnt specified");
            }
        }
    }
}
