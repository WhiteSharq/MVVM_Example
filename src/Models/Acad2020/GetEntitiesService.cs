namespace WS.Models.Acad2020
{
    using System.Linq;
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.DatabaseServices.IoC;
    using Contracts;

#pragma warning disable CS0618
    public class GetEntitiesService : IGetEntities
    {
        private IDatabase _database;
        private EntityToDTOConverter _entityToDTOConverter;

        public GetEntitiesService(
            IDatabase database,
            EntityToDTOConverter entityToDTOConverter)
        {
            _database = database;
            _entityToDTOConverter = entityToDTOConverter;
        }

        public EntityDTO[] GetEntities(string namePart)
        {
            var bt = (BlockTable)
                _database.BlockTableId
                    .Open(OpenMode.ForRead);

            var modelSpace = (BlockTableRecord)
                bt[BlockTableRecord.ModelSpace]
                    .Open(OpenMode.ForRead);

            var dbObjs = modelSpace
                .Cast<ObjectId>()
                .Select(id => id.Open(OpenMode.ForRead))
                .ToList();

            var uppered = namePart.ToUpper();

            var res = dbObjs
                .OfType<Entity>()
                .Where(e => e.GetRXClass().Name.ToString().ToUpper().Contains(uppered))
                .Select(_entityToDTOConverter.Convert)
                .ToArray();

            dbObjs.ForEach(dbo => dbo.Close());

            modelSpace.Close();

            bt.Close();

            return res;
        }
    }
#pragma warning restore CS0618
}
