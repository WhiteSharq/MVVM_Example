namespace WS.Models.Revit2020
{
    using Autodesk.Revit.ApplicationServices;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.DB.Events;
    using Contracts;
    using System;
    using System.Linq;

    public class WatchDocumentService : IWatchDocument
    {
        private Application _application;

        public WatchDocumentService(Application application)
        {
            _application = application;
        }

        public event Action<bool> Activated;

        public void Start()
        {
            _application.DocumentOpened +=
                On_Application_DocumentOpened;

            // may be a wrong to detect if a doc is active
            var anyDocIsActive = _application
                .Documents
                .Cast<Document>()
                .Any(d => d is not null);

            Activated?.Invoke(anyDocIsActive);
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        private void On_Application_DocumentOpened(
            object sender,
            DocumentOpenedEventArgs e)
        {
            Activated?.Invoke(e.Document is not null);
        }
    }
}