using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Data;

namespace MagicVilla_VillaAPI.Repository
{
    public class UserRepository : IUserRepository
    {

        private ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.localUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTOcs> Login(LoginRequestDTOcs loginrequestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            LocalUser user = new LocalUser();
            user.UserName = registrationRequestDTO.UserName;
            user.Password = registrationRequestDTO.Password;
            user.Role = registrationRequestDTO.Role;
            user.Name = registrationRequestDTO.Name;

            _db.localUsers.Add(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;

        }
    }
}
