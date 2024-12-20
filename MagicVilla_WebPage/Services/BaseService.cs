using MagicVilla_utility;
using MagicVilla_WebPage.Models;
using MagicVilla_WebPage.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MagicVilla_WebPage.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get; set;}
        public IHttpClientFactory httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new();
            this.httpClient = httpClient; 

        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
           try
            {
                var client = httpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case SD.ApiType.DELTE:
                        message.Method = HttpMethod.Delete;
                        break;
                }

                HttpResponseMessage apiResponse = null;
                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                try
                {
                    APIResponse APiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest
                    || apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        APiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        APiResponse.IsSuccess = false;

                        var res = JsonConvert.SerializeObject(APiResponse);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);
                        return returnObj;

                    }
                }
                catch (Exception)
                {

                    var exceptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return exceptionResponse;
                }


                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;



            }
            catch
            (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
