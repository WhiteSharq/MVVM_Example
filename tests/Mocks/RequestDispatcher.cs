using Contracts;
using System.Threading.Tasks;

public class RequestDispatcher : ICanHandleRequest
{
    public Task<object?> Handle(string request)
    {
        return Task.FromResult<object?>("{}");
    }
}