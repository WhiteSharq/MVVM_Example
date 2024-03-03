#nullable enable

namespace WS.Models.Revit2020
{
    using Autodesk.Revit.DB;
    using Contracts;

    public class ElementToDTOConverter
    {
        public EntityDTO Convert(Element element)
        {
            var res = new EntityDTO(
                element.Id.IntegerValue.ToString(),
                element.Name,
                TryGetLocation(element));

            return res;
        }

        private PointDTO? TryGetLocation(Element e)
        {
            return e.Location is LocationPoint lp ?
                new PointDTO(lp.Point.X, lp.Point.X, lp.Point.X) :
                null;
        }
    }
}
