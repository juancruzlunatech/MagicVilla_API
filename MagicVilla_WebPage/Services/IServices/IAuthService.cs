using MagicVilla_WebPage.Models.Dto;


namespace MagicVilla_WebPage.Services.IServices
{
    public interface IAuthService
    {
        Task<T> RegisterAsync<T>(RegistrationRequestDTO user);
        Task<T> LoginAsync<T>(LoginRequestDTO loginRequest);

    }
}
