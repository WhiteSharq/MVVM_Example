namespace WS.Models.Acad2020
{
    using Autodesk.AutoCAD.ApplicationServices;
    using Contracts;
    using System.Reflection;

    public class ZoomEntityService : IZoomEntity
    {
        public void Zoom(EntityDTO entity)
        {
            if (entity.Location is not PointDTO cp)
            {
                return;
            }

            object acad = Application.AcadApplication;

            const BindingFlags flags =
                BindingFlags.DeclaredOnly |
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.InvokeMethod;

            var xyz = new double[] { cp.X, cp.Y, cp.Z };

            double magnitude = 100;

            acad.GetType().InvokeMember(
                "ZoomCenter",
                flags,
                null,
                acad,
                new object[] { xyz, magnitude });
        }
    }
}