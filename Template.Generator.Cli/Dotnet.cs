using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Template.Generator.Cli
{
    public class DotnetCli
    {
        private ProcessStartInfo _info;
        private DataReceivedEventHandler _errorDataReceived;
        private StringBuilder _stderr;
        private StringBuilder _stdout;
        private DataReceivedEventHandler _outputDataReceived;
        private bool _anyNonEmptyStderrWritten;

        public static DotnetCli Restore(params string[] args)
        {
            return new DotnetCli
            {
                _info = new ProcessStartInfo("dotnet", ArgumentEscaper.EscapeAndConcatenateArgArrayForProcessStart(new[] { "restore" }.Concat(args)))
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };
        }

        public static DotnetCli New(string alias, params string[] args)
        {
            return new DotnetCli
            {
                _info = new ProcessStartInfo("dotnet", ArgumentEscaper.EscapeAndConcatenateArgArrayForProcessStart(new[] { "new", alias }.Concat(args)))
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
        }

        public static DotnetCli AddProjectToProjectReference(string projectFile, params string[] args)
        {
            return new DotnetCli
            {
                _info = new ProcessStartInfo("dotnet", ArgumentEscaper.EscapeAndConcatenateArgArrayForProcessStart(new[] { "add", projectFile, "reference" }.Concat(args)))
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };
        }

        public static DotnetCli AddPackageReference(string projectFile, string packageName, string version = null)
        {
            string argString;
            if (version == null)
            {
                argString = ArgumentEscaper.EscapeAndConcatenateArgArrayForProcessStart(new[] { "add", projectFile, "package", packageName, "--no-restore"});
            }
            else
            {
                argString = ArgumentEscaper.EscapeAndConcatenateArgArrayForProcessStart(new[] { "add", projectFile, "package", packageName, "--version", version, "--no-restore"});
            }

            return new DotnetCli
            {
                _info = new ProcessStartInfo("dotnet", argString)
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };
        }

        public static DotnetCli AddProjectsToSolution(string solutionFile, IReadOnlyList<string> projects)
        {
            var allArgs = new List<string>() {
                "sln",
                solutionFile,
                "add"
            };
            allArgs.AddRange(projects);
            string argString = ArgumentEscaper.EscapeAndConcatenateArgArrayForProcessStart(allArgs);
            return new DotnetCli
            {
                _info = new ProcessStartInfo("dotnet", argString)
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };
        }

        public DotnetCli ForwardStdErr()
        {
            _errorDataReceived = ForwardStreamStdErr;
            return this;
        }

        public DotnetCli ForwardStdOut()
        {
            _outputDataReceived = ForwardStreamStdOut;
            return this;
        }

        private void ForwardStreamStdOut(object sender, DataReceivedEventArgs e)
        {
            Console.Out.WriteLine(e.Data);
        }

        private void ForwardStreamStdErr(object sender, DataReceivedEventArgs e)
        {
            if (!_anyNonEmptyStderrWritten)
            {
                if (string.IsNullOrWhiteSpace(e.Data))
                {
                    return;
                }

                _anyNonEmptyStderrWritten = true;
            }

            Console.Error.WriteLine(e.Data);
        }

        public DotnetCli CaptureStdOut()
        {
            _stdout = new StringBuilder();
            _outputDataReceived += CaptureStreamStdOut;
            return this;
        }

        private void CaptureStreamStdOut(object sender, DataReceivedEventArgs e)
        {
            _stdout.AppendLine(e.Data);
        }

        public DotnetCli CaptureStdErr()
        {
            _stderr = new StringBuilder();
            _errorDataReceived += CaptureStreamStdErr;
            return this;
        }

        private void CaptureStreamStdErr(object sender, DataReceivedEventArgs e)
        {
            _stderr.AppendLine(e.Data);
        }

        public Result Execute()
        {
            var p = Process.Start(_info);
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.ErrorDataReceived += OnErrorDataReceived;
            p.OutputDataReceived += OnOutputDataReceived;
            p.WaitForExit();

            return new Result(_stdout?.ToString(), _stderr?.ToString(), p.ExitCode);
        }

        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            _outputDataReceived?.Invoke(sender, e);
        }

        private void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            _errorDataReceived?.Invoke(sender, e);
        }

        public static DotnetCli Version()
        {
            return new DotnetCli
            {
                _info = new ProcessStartInfo("dotnet", "--version")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };
        }
    }
}