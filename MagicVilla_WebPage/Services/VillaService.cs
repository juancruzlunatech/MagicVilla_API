using MagicVilla_utility;
using MagicVilla_WebPage.Models.Dto;
using MagicVilla_WebPage.Services.IServices;

namespace MagicVilla_WebPage.Services
{
    public class VillaService : BaseService, IVillaService
    {

        private readonly IHttpClientFactory _clientfactory;
        private string villaUrl;    
        
        public VillaService(IHttpClientFactory clientfactory, IConfiguration configuration):base(clientfactory) 
        {
            _clientfactory = clientfactory;
            villaUrl = configuration.GetValue<string>("ServicesUrls:VillaApi");
        }
        public Task<T> CreateAsync<T>(CreateVillaDTO dto, string token)
        {
            return SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl + "api/v1/VillaAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.DELTE,
                Url = villaUrl + "api/v1/VillaAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "api/v1/VillaAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "api/v1/VillaAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(UpdateVillaDTO dto, string token)
        {
            return SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "api/v1/VillaAPI/" + dto.Id,
                Token = token
            });
        }
    }
}
