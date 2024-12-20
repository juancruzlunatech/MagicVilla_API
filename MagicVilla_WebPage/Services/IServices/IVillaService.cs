using MagicVilla_WebPage.Models.Dto;

namespace MagicVilla_WebPage.Services.IServices
{
    public interface IVillaService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(CreateVillaDTO dto, string token);
        Task<T> UpdateAsync<T>(UpdateVillaDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
