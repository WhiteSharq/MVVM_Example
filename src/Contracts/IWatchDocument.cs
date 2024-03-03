namespace Contracts
{
    using System;

    public interface IWatchDocument
    {
        public event Action<bool> Activated;
        public void Start();
        public void Stop();
    }
}
