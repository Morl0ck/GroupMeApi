using System.Net;

namespace PME.GroupMeApi
{
    public class Bots
    {
        private string bot_id { get; set; }

        public Bots(string bot_id)
        {
            this.bot_id = bot_id;
        }

        public string PostMessage(string text)
        {
            string actionUrl = "/bots/post";

            string postData = "{\"bot_id\": \""+ bot_id + "\", \"text\": \""+ text + "\"}";

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json;charset=UTF-8";
                return client.UploadString(Constants.GetApiUrl(actionUrl), "POST", postData);
            }
        }
    }
}
