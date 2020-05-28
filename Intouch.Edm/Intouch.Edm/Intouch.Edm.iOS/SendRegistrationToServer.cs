using System;

namespace Intouch.Edm.iOS
{
    public class SendRegistrationToServer
    {
        public static void Save(string token)
        {
            try
            {
                Helpers.Settings.FirebaseNotification = token;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}