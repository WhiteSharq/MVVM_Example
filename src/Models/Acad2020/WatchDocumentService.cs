namespace WS.Models.Acad2020
{
    using Autodesk.AutoCAD.ApplicationServices;
    using Contracts;
    using System;

    public class WatchDocumentService : IWatchDocument
    {
        private void On_DocumentManager_DocumentActivated(
            object sender,
            DocumentCollectionEventArgs e)
        {
            Activated?.Invoke(e.Document is null);
        }

        public event Action<bool> Activated;

        public void Start()
        {
            Application.DocumentManager.DocumentActivated +=
                On_DocumentManager_DocumentActivated;

            Activated?.Invoke(true); // for testing
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
