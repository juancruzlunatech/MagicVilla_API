using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Repository
{
    public interface IUserRepository
    {
        public bool IsUniqueUser(string username);

        Task<LoginResponseDTOcs> Login(LoginRequestDTOcs loginrequestDTO );

        Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
