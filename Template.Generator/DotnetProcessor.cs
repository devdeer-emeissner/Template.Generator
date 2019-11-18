using System;

namespace Template.Generator
{
    public class DotnetConfigProcessor : IDisposable
    {
        private bool _isDisposed;
        public DotnetConfigProcessor()
        {
            Dispose(false);
        }
        
        public void Dispose()
        {
           Dispose(true) 
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            else
            {
                _isDisposed = true;
            }
        }
    }
}