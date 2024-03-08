namespace Contracts
{
    public abstract class RequestDTO<TService, TParam>
    {
        public RequestDTO(TParam argument)
        {
            ServiceType = typeof(TService).Name;
            Argument = argument;
            ParameterType = typeof(TParam).Name;
            Method = GetServiceMethodName();
        }

        public TParam Argument { get; }

        public string Method { get; set; }

        public string ParameterType { get; }

        public string ServiceType { get; }

        public abstract string GetServiceMethodName();
    }
}