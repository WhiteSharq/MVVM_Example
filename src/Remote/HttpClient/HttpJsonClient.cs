namespace WS.HttpClient
{
    using Contracts;
    using Newtonsoft.Json;
    using System.Net.Http;

    public class HttpJsonClient : IGetEntities, IZoomEntity
    {
        private HttpClient _httpClient;

        public EntityDTO[] GetEntities(string namePart)
        {
            var request = new EnititesRequestDTO(namePart);

            var responseString = MakeRequest(request);

            var ents = JsonConvert
                .DeserializeObject<EntityDTO[]>(
                    responseString);

            return ents;
        }

        public void Zoom(EntityDTO entity)
        {
            var request = new ZoomRequestDTO(entity);

            var stringContent = MakeRequest(request);
        }

        private string MakeRequest<TService,TInput>(
            RequestDTO<TService, TInput> request)
        {
            var objectAsString = JsonConvert
                .SerializeObject(request);

            var stringContent =
                new StringContent(objectAsString);

            var resp = _httpClient.PostAsync("", stringContent).Result;

            var str = resp.Content.ReadAsStringAsync().Result;

            return str;
        }
    }
}