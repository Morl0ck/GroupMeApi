using System;
using System.Collections.Generic;
using System.Text;

namespace PME.GroupMeApi
{
    public static class Constants
    {
        // Documentation: https://dev.groupme.com/docs/v3

        public static string baseUrl = "https://api.groupme.com/v3";
        public static string token = "";

        public static string GetApiUrl()
        {
            return baseUrl + "?token=" + token;
        }

        public static string GetApiUrl (string actionUrl) {
            return baseUrl + actionUrl + "?token=" + token;
        }
    
    }
}
