#nullable enable

namespace WS.Models.Acad2020
{
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.EditorInput;
    using Autodesk.AutoCAD.EditorInput.IoC;
    using Contracts;
    using System.Linq;

#pragma warning disable CS0618
    public class PickEntitiesService : IPickEntities
    {
        private IEditor _editor;
        private EntityToDTOConverter _entityToDTOConverter;

        public PickEntitiesService(
            IEditor editor,
            EntityToDTOConverter entityToDTOConverter)
        {
            _editor = editor;
            _entityToDTOConverter = entityToDTOConverter;
        }

        public EntityDTO[] Select(object? parameters)
        {
            var opts = new PromptSelectionOptions()
            {
                AllowDuplicates = false,
            };

            var selRes = _editor.GetSelection(opts);

            if (selRes.Status != PromptStatus.OK)
            {
                return new EntityDTO[0];
            }

            var dbObjs = selRes.Value
                .GetObjectIds()
                .Select(id => id.Open(OpenMode.ForRead))
                .ToList();

            var res = dbObjs
                .OfType<Entity>()
                .Select(_entityToDTOConverter.Convert)
                .ToArray();

            dbObjs.ForEach(dbo => dbo.Close());

            return res;
        }
    }
#pragma warning restore CS0618    
}
