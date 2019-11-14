using System.Diagnostics;
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
    }
}