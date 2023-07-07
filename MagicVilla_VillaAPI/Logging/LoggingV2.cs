namespace MagicVilla_VillaAPI.Logging
{
    public class LoggingV2 : ILogging
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

            //This is not implemented but here have to be the implementation if we want to build a diferente logger
        }
    }
}
