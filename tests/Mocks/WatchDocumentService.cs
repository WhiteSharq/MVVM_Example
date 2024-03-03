using Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

public class WatchDocumentService : IWatchDocument
{
    public WatchDocumentService()
    {
    }

    public async void Start()
    {
        var r = new Random();

        var active = true;

        while (true)
        {
            var n = r.Next(0, 100);

            await Task.Delay(n)
                .ContinueWith(_ => Activated.Invoke(active))
                .ConfigureAwait(false);

            active = !active;
        }
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }

    public event Action<bool> Activated;
}