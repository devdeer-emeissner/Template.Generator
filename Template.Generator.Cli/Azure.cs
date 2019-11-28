using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Template.Generator.Cli
{
    public class Azure
    {  
        private ProcessStartInfo _info;
        private DataReceivedEventHandler _errorDataReceived;
        private StringBuilder _stderr;
        private StringBuilder _stdout;
        private DataReceivedEventHandler _outputDataReceived;
        private bool _anyNonEmptyStderrWritten;

        public static Azure Login(params string[] args)
        {
            return new Azure
            {
                _info = new ProcessStartInfo("az", ArgumentEscaper.EscapeAndConcatenateArgArrayForProcessStart(new[] { "login" }.Concat(args)))
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };
        }

        public static Azure Command(params string[] args)
        {
            return new Azure()
            {
                _info = new ProcessStartInfo("az", ArgumentEscaper.EscapeAndConcatenateArgArrayForProcessStart(args))
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };
        }

        public static Azure CreateResourceGroup(string name, string location, params string[] args)
        {
            return new Azure
            {
                _info = new ProcessStartInfo("az", ArgumentEscaper.EscapeAndConcatenateArgArrayForProcessStart(new[] { "group", "create", "--name", name, "--location", location }.Concat(args)))
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };
        }

        public static Azure CreateWebApp(string name, string resourceGroup, string plan, params string[] args)
        {
            return new Azure
            {
                _info = new ProcessStartInfo("az", ArgumentEscaper.EscapeAndConcatenateArgArrayForProcessStart(new[] { "webapp", "create", "--name", name, "--plan", plan, "--resource-group", resourceGroup }.Concat(args)))
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };
        }
        public static Azure CreateAppServicePlan(string name, string resourceGroup, params string[] args)
        {
            return new Azure
            {
                _info = new ProcessStartInfo("az", ArgumentEscaper.EscapeAndConcatenateArgArrayForProcessStart(new[] { "appservice", "plan", "create", "--name", name, "--resource-group", resourceGroup }.Concat(args)))
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };
        }

        public static Azure DeployArmTemplate(string resourceGroup, string pathToTemplate, params string[] args)
        {
            return new Azure
            {
                _info = new ProcessStartInfo("az", ArgumentEscaper.EscapeAndConcatenateArgArrayForProcessStart(new[] { "group", "deployment", "create", "--resource-group", resourceGroup, "--template-file", pathToTemplate }.Concat(args)))
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true 
                }
            };
        }

        public Azure ForwardStdErr()
        {
            _errorDataReceived = ForwardStreamStdErr;
            return this;
        }

        public Azure ForwardStdOut()
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

        public Azure CaptureStdOut()
        {
            _stdout = new StringBuilder();
            _outputDataReceived += CaptureStreamStdOut;
            return this;
        }

        private void CaptureStreamStdOut(object sender, DataReceivedEventArgs e)
        {
            _stdout.AppendLine(e.Data);
        }

        public Azure CaptureStdErr()
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

        public static Azure Version()
        {
            return new Azure
            {
                _info = new ProcessStartInfo("az", "--version")
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