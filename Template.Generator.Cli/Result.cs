namespace Template.Generator.Cli
{
    public class Result
        {
            public Result(string stdout, string stderr, int exitCode)
            {
                StdErr = stderr;
                StdOut = stdout;
                ExitCode = exitCode;
            }

            public string StdErr { get; }

            public string StdOut { get; }

            public int ExitCode { get; }
        }
}