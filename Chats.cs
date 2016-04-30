using SimpleLogger;
using System.Net;

namespace PME.GroupMeApi
{
    public class Chats
    {
        /// <summary>
        /// Returns a paginated list of direct message chats, or conversations, sorted by updated_at descending.
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="per_page">Number of chats per page</param>
        /// <returns></returns>
        public string Index(int page, int per_page)
        {
            string actionUrl = "/chats";

            Logger.Log("Starting Web Call -> Index");

            string getData = "";
            if (page != 0)
                getData += string.Format("&page={0}", page);
            if (per_page != 0)
                getData += string.Format("&per_page={0}", per_page);

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json;charset=UTF-8";
                return client.DownloadString(Constants.GetApiUrl(actionUrl) + getData);
            }
        }

        public string Index(int page)
        {
            return Index(page, 0);
        }

        public string Index()
        {
            return Index(0, 0);
        }
    }
}
