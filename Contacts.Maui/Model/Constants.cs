using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Maui.Model
{
    public static class Constants
    {
        //The Application or Client ID will be generated while registering the app in the Azure portal. Copy and paste the GUID.
        public static readonly string ClientId = "9425adef-a017-4535-ba06-73a0ea746144";

        //Leaving the scope to its default values.
        public static readonly string[] Scopes = new string[] { "openid", "offline_access" };
    }
}
