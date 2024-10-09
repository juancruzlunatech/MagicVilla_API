using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace MagicVilla_VillaAPI.Repository
{
    public class UserRepository : IUserRepository
    {

        private ApplicationDbContext _db;
        private string secretKey;

        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            //secretKey = configuration.GetSection("AppSettings:Secret").Value;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
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
            var user = _db.localUsers.FirstOrDefault(x => x.UserName.ToLower() == loginrequestDTO.UserName.ToLower() && x.Password == loginrequestDTO.Password);
            if (user == null)
            {
                return new LoginResponseDTOcs()
                {
                    User = null,
                    Token = ""
                };
            }
            //if user is found generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTOcs loginResponseDTOcs = new LoginResponseDTOcs()
            {
                User = user,
                Token = tokenHandler.WriteToken(token)
            };
            return loginResponseDTOcs;
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
