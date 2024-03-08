namespace WS.Remote
{
    using Contracts;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public class HttpJsonServer : IDisposable
    {
        protected HttpListener _listener;
        private bool _isConfigured;
        private ICanHandleRequest _requestHandler;
        private bool _shouldListen;

        public HttpJsonServer(
            ICanHandleRequest requestHandler,
            HttpListener listener)
        {
            if (!HttpListener.IsSupported)
            {
                throw new InvalidOperationException(
                    "Unsupported operating system!");
            }

            _requestHandler = requestHandler;
            _listener = listener;
        }

        public void Dispose()
        {
            _listener.Stop();
            _listener.Close();
        }

        public async void Start()
        {
            if (!_isConfigured)
            {
                Configure();
            }

            if (_listener.IsListening)
            {
                return;
            }
            else
            {
                Exception? startEx = null;

                await Task.Run(() =>
                {
                    try
                    {
                        _listener.Start();
                    }
                    catch (Exception ex)
                    {
                        startEx = ex;
                    }
                });

                if (startEx != null)
                {
                    throw startEx;
                }

                _shouldListen = true;
            }

            while (_shouldListen)
            {
                try
                {
                    var context = await _listener
                        .GetContextAsync();

                    var url = context
                        .Request
                        .RawUrl
                        .Replace("/", string.Empty)
                        .ToUpper();

                    if (url == "STOP")
                    {
                        Dispose();
                        break;
                    }

                    var response = context.Response;

                    if (url.Contains("FAVICON.ICO"))
                    {
                        response.StatusCode = (int)
                            HttpStatusCode.BadRequest;

                        response.OutputStream.Close();

                        continue;
                    }

                    var request = context.Request;

                    string body;

                    var reader = new StreamReader(
                        request.InputStream,
                        request.ContentEncoding);

                    using (reader)
                    {
                        body = reader.ReadToEnd();
                    }

                    if (body.Length < 2)
                    {
                        response.StatusCode =
                            (int)HttpStatusCode.BadRequest;

                        response.OutputStream.Close();

                        return;
                    }

                    var result = await _requestHandler
                        .Handle(body);

                    if (result is null)
                    {
                        response.StatusCode =
                            (int)HttpStatusCode.NotFound;
                    }
                    else
                    {
                        var responseString = JsonConvert
                            .SerializeObject(result);

                        response.StatusCode =
                            (int)HttpStatusCode.OK;

                        var buffer = Encoding.UTF8
                            .GetBytes(responseString);

                        response.ContentLength64 = buffer.Length;

                        response.OutputStream.Write(buffer, 0, buffer.Length);
                        
                        response.OutputStream.Close();
                    }
                }
                catch (HttpListenerException)
                { }
                catch (Exception ex)
                {
                }
            }
        }

        public void Stop()
        {
            _shouldListen = false;
        }

        private void Configure()
        {
            if (_isConfigured) { return; }

            var url = "http://127.0.0.1";
            string port = "8080";
            string prefix = $"{url}:{port}/";
            _listener.Prefixes.Add(prefix);
            _isConfigured = true;
        }
    }
}