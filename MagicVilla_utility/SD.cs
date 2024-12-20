using System.Security.Cryptography.X509Certificates;

namespace MagicVilla_utility
{
    public static class SD
    {
        public enum ApiType
        { 
            GET,
            POST,
            PUT,
            DELTE
        }
        public static string SessionToken = "JWTToken";
    }
}