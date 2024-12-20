using MagicVilla_utility;
using MagicVilla_WebPage.Models.Dto;

using MagicVilla_WebPage.Services.IServices;

namespace MagicVilla_WebPage.Services
{
    public class AuthService: BaseService, IAuthService
    {

        private readonly IHttpClientFactory _clientfactory;
        private string villaUrl;

        public AuthService(IHttpClientFactory clientfactory, IConfiguration configuration) : base(clientfactory)
        {
            _clientfactory = clientfactory;
            villaUrl = configuration.GetValue<string>("ServicesUrls:VillaApi");
        }

        public Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "api/UsersAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO obj)
        {
            return SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "api/UsersAuth/register"
            });
        }
    }
}
