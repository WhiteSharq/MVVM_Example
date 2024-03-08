namespace WS.Models.Revit2020
{
    using Contracts;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class RequestDispatcher : ICanHandleRequest
    {
        private IGetEntities _getEntitiesService;
        private IPickEntities _pickEntitiesService;

        private object[] _services;

        public RequestDispatcher(
            IGetEntities getEntitiesService,
            IPickEntities pickEntitiesService)
        {
            _getEntitiesService = getEntitiesService;
            _pickEntitiesService = pickEntitiesService;
        }

        public RequestDispatcher(IGetEntities getEntitiesService)
        {
            _services = new object[] { getEntitiesService };
        }

        public Task<object?> Handle(string request)
        {
            JObject json;

            try
            {
                json = JObject.Parse(request);
            }
            catch (Exception)
            {
                return ExcepWithMessage(
                    $"Malformed request. " +
                    $"Must be a valid JSON!");
            }

            var serviceName = json
                [nameof(RequestDTO<int, int>.ServiceType)]
                .Value<string>();

            if (string.IsNullOrWhiteSpace(serviceName))
            {
                return ExcepWithMessage(
                    $"Incorrect service " +
                    $"name \"{serviceName}\"");
            }

            var methodName = json
                [nameof(RequestDTO<int, int>.Method)]
                .Value<string>();

            if (string.IsNullOrWhiteSpace(methodName))
            {
                return ExcepWithMessage(
                    $"Incorrect method " +
                    $"name \"{methodName}\"");
            }

            var parameterTypeName = json
                [nameof(RequestDTO<int, int>.ParameterType)]
                .Value<string>();

            if (string.IsNullOrWhiteSpace(parameterTypeName))
            {
                return ExcepWithMessage(
                    $"Incorrect method " +
                    $"type parameter name " +
                    $"\"{parameterTypeName}\"");
            }

            object arg;

            if (parameterTypeName == nameof(String))
            {
                arg = json
                    [nameof(RequestDTO<int, int>.Argument)]
                    .Value<string>();
            }
            else
            {
                throw new Exception();
            }

            var service = _services.SingleOrDefault(
                o => o.GetType()
                      .GetInterface(serviceName, true) is not null);

            if (service is null)
            {
                return ExcepWithMessage(
                    $"Unsupported service " +
                    $"\"{serviceName}\"");
            }

            var method = service
                .GetType()
                .GetMethod(methodName);

            if (method is null)
            {
                return ExcepWithMessage(
                    $"Unsupported method " +
                    $"\"{methodName}\"");
            }

            var result = method.Invoke(
                service,
                new object[] { arg });

            return Task.FromResult<object?>(result);
        }

        private Task<object?> ExcepWithMessage(string message)
        {
            return Task.FromResult(
                (object?)new Exception(message));
        }
    }
}
