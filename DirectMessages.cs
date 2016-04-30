using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PME.GroupMeApi
{
    /// <summary>
    /// Direct Messages Api
    /// <para>https://dev.groupme.com/docs/v3#direct_messages</para>
    /// </summary>
    public class DirectMessages
    {
        protected string actionUrl = "/direct_messages";

        /// <summary>
        /// Fetch direct messages between two users.
        /// DMs are returned in groups of 20, ordered by created_at descending.
        /// If no messages are found(e.g.when filtering with since_id) we return code 304.
        /// Note that for historical reasons, likes are returned as an array of user ids in the favorited_by key.
        /// </summary>
        /// <param name="other_user_id" required="true">The other participant in the conversation.</param>
        /// <param name="before_id">Returns 20 messages created before the given message ID</param>
        /// <param name="since_id">Returns 20 messages created after the given message ID</param> 
        /// <returns></returns>
        public string Index(string other_user_id, string message_id, bool before = true)
        {
            Logger.Log("Starting Web Call -> Index");

            string getData = string.Format("&other_user_id={0}", other_user_id);
            if (!string.IsNullOrEmpty(message_id))
            {
                getData += string.Format("&{0}={1}", (before) ? "before_id" : "since_id", message_id);
            }

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json;charset=UTF-8";
                return client.DownloadString(Constants.GetApiUrl(actionUrl) + getData);
            }
        }

        public string Index(string other_user_id)
        {
            return Index(other_user_id, null);
        }

        public string Create(string recipient_id, string text)
        {
            Logger.Log("Starting Web Call -> Create");

            string guid = Guid.NewGuid().ToString().Replace("-", "");

            string postData = "{\"direct_message\":{\"source_guid\": \""+ guid + "\", \"recipient_id\": \""+ recipient_id + "\", \"text\": \""+ text + "\"}}";

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json;charset=UTF-8";
                return client.UploadString(Constants.GetApiUrl(actionUrl), "POST", postData);
            }
        }
    }
}
