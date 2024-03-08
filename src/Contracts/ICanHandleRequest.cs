namespace Contracts
{
    using System.Threading.Tasks;

    public interface ICanHandleRequest
    {
        public Task<object?> Handle(string request);
    }
}
