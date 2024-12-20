using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Repository
{
    public interface IUserRepository
    {
        public bool IsUniqueUser(string username);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginrequestDTO );

        Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
