using MagicVilla_WebPage.Models;

namespace MagicVilla_WebPage.Services.IServices
{
    public interface IBaseService
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest); 

    }
}
