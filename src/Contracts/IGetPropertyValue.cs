namespace Contracts
{
    public interface IGetPropertyValue
    {
        public string? GetPropertyValue(string entityId, string propertyName, object? units);
    }
}
