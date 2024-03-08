namespace Contracts
{
    public class ZoomRequestDTO :
        RequestDTO<IZoomEntity, object>
    {
        public ZoomRequestDTO(EntityDTO entity)
            : base(entity)
        {
        }

        public override string GetServiceMethodName()
            => nameof(IZoomEntity.Zoom);
    }
}
