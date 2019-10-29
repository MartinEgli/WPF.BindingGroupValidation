namespace WpfApp3
{
    #region

    using System;

    #endregion

    public class Disposable : IDisposable
    {
        private readonly Action action;

        public Disposable(Action action)
        {
            this.action = action;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.action();
            }
        }
    }
}