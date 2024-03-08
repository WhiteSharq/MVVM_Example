namespace Contracts
{
    public class PickRequestDTO :
        RequestDTO<IPickEntities, object>
    {
        public PickRequestDTO(object parameters)
            : base(parameters)
        {
        }

        public override string GetServiceMethodName()
            => nameof(IPickEntities.Select);
    }
}
