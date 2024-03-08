namespace Contracts
{
    public class EnititesRequestDTO :
        RequestDTO<IGetEntities, string>
    {
        public EnititesRequestDTO(string namePart)
            : base(namePart)
        {
        }

        public override string GetServiceMethodName()
            => nameof(IGetEntities.GetEntities);
    }
}
