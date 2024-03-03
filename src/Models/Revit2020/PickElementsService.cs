namespace WS.Models.Revit2020
{
    using Autodesk.Revit.UI;
    using Autodesk.Revit.UI.Selection;
    using Contracts;
    using System.Linq;

    public class PickElementsService : IPickEntities
    {
        private UIDocument _uiDocument;
        private ElementToDTOConverter _elementToDTOConverter;

        public PickElementsService(
            UIDocument uiDocument,
            ElementToDTOConverter elementToDTOConverter)
        {
            _uiDocument = uiDocument;
            _elementToDTOConverter = elementToDTOConverter;
        }

        public EntityDTO[] Select(object parameters)
        {
            var doc = _uiDocument.Document;

            var res = _uiDocument.Selection
                .PickObjects(ObjectType.Element)
                .Select(doc.GetElement)
                .Select(_elementToDTOConverter.Convert)
                .ToArray();

            return res;
        }
    }
}
