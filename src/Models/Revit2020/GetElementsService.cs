#nullable enable

namespace WS.Models.Revit2020
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Contracts;
    using System.Linq;

    public class GetElementsService : IGetEntities
    {
        private UIDocument _uiDocument;
        private ElementToDTOConverter _elementToDTOConverter;

        public GetElementsService(
            UIDocument uiDocument,
            ElementToDTOConverter elementToDTOConverter)
        {
            _uiDocument = uiDocument;
            _elementToDTOConverter = elementToDTOConverter;
        }

        public EntityDTO[] GetEntities(string namePart)
        {
            var doc = _uiDocument.Document;

            var uppered = namePart.ToUpper();

            var elements = new FilteredElementCollector(
                    doc,
                    doc.ActiveView.Id)
                .WhereElementIsNotElementType()
                .Where(e => e.Name is string name &&
                            name.ToUpper().Contains(uppered))
                .Select(_elementToDTOConverter.Convert)
                .ToArray();

            return elements;
        }
    }
}
