using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

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
            }
        }
    }
}