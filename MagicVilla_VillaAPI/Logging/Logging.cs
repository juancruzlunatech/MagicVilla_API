using Serilog;

namespace MagicVilla_VillaAPI.Logging
{
    public class Logging : ILogging
    {

        public void Log(string message, string type)
        { 
            if (type == null) 
            {
                Console.WriteLine("there is an Error" + message); 
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}
