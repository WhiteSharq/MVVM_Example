#nullable enable

namespace WS.Models.Acad2020
{
    using Autodesk.AutoCAD.DatabaseServices;
    using Contracts;

    public class EntityToDTOConverter
    {
        public EntityDTO Convert(Entity entity)
        {
            var res = new EntityDTO(
                entity.Id.Handle.Value.ToString(),
                entity.Id.ObjectClass.Name.ToString(),
                TryGetLocation(entity));

            return res;
        }

        private PointDTO? TryGetLocation(Entity e)
        {
            try
            {
                var ext = e.GeometricExtents;

                var hv = 0.5 * (ext.MaxPoint - ext.MinPoint);

                var cp = ext.MinPoint + hv;

                return new PointDTO(cp.X, cp.Y, cp.Z);
            }
            catch
            {
                return null;
            }
        }
    }
}
