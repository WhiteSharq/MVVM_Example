namespace Contracts
{
    public class PropertyDTO
    {
        public PropertyDTO(string name, string? value, string? entityId)
        {
            Name = name;
            Value = value;
            EntityId = entityId;
        }

        public string Name { get; }
        public string? Value { get; }
        public string? EntityId { get; }
    }
}
