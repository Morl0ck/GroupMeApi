using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace PME.GroupMeApi
{
    /// <summary>
    /// Members Api
    /// <para>https://dev.groupme.com/docs/v3#members</para>
    /// </summary>
    public class Members
    {
        private List<Member> members;
        public string group_id { get; set; }

        /// <summary>
        /// Basic Constructor. Cannot use shorthand overloads.
        /// <para>Consider using Members(string group_id)</para>
        /// </summary>
        public Members() { }

        /// <summary>
        /// Sets the group_id in the class so that the shorthand overloads can be used.
        /// </summary>
        /// <param name="group_id"></param>
        public Members(string group_id)
        {
            this.group_id = group_id;
        }

        /// <summary>
        /// Add members to a group.
        /// <para>Multiple members can be added in a single request, and results are fetched with a separate call (since memberships are processed asynchronously).</para>
        /// <para>The response includes a results_id that's used in the results request.</para>
        /// <para>In order to correlate request params with resulting memberships, GUIDs can be added to the members parameters.</para>
        /// <para>These GUIDs will be reflected in the membership JSON objects.</para>
        /// </summary>
        /// <param name="group_id"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        public string Add(string group_id, List<Member> members)
        {
            this.members = members;

            string actionUrl = string.Format("/groups/{0}/members/add", group_id);

            string postData = JsonConvert.SerializeObject(this);

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json;charset=UTF-8";
                return client.UploadString(Constants.GetApiUrl(actionUrl), "POST", postData);
            }
        }

        /// <summary>
        /// This requires you use the constructor Members(string group_id)
        /// <para>See Add(string group_id, List<Member> members) for details</para>
        /// </summary>
        /// <param name="members"></param>
        /// <returns></returns>
        public string Add(List<Member> members)
        {
            return Add(this.group_id, members);
        }

        /// <summary>
        /// Get the membership results from an add call.
        /// <para>Successfully created memberships will be returned, including any GUIDs that were sent up in the add request.</para>
        /// <para>If GUIDs were absent, they are filled in automatically. Failed memberships and invites are omitted.</para>
        /// <para>Keep in mind that results are temporary -- they will only be available for 1 hour after the add request.</para>
        /// </summary>
        /// <param name="group_id"></param>
        /// <param name="results_id">This is the guid that's returned from an add request.</param>
        /// <returns></returns>
        public string Results(string group_id, string results_id)
        {
            string actionUrl = string.Format("/groups/{0}/members/results/{1}", group_id, results_id);

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json;charset=UTF-8";
                return client.DownloadString(Constants.GetApiUrl(actionUrl));
            }
        }

        /// <summary>
        /// This requires you use the constructor Members(string group_id)
        /// <para>See Results(string group_id, string results_id) for details</para>
        /// </summary>
        /// <param name="results_id"></param>
        /// <returns></returns>
        public string Results(string results_id)
        {
            return Results(this.group_id, results_id);
        }

        /// <summary>
        /// Remove a member (or yourself) from a group.
        /// <para>Note: The creator of the group cannot be removed or exit.</para>
        /// </summary>
        /// <param name="group_id"></param>
        /// <param name="membership_id">Please note that this isn't the same as the user ID. In the members key in the group JSON, this is the id value, not the user_id.</param>
        /// <returns></returns>
        public string Remove(string group_id, string membership_id)
        {
            string actionUrl = string.Format("/groups/{0}/members/{1}/remove", group_id, membership_id);

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json;charset=UTF-8";
                return client.UploadString(Constants.GetApiUrl(actionUrl), "POST");
            }
        }

        /// <summary>
        /// This requires you use the constructor Members(string group_id)
        /// <para>See Remove(string group_id, string membership_id) for details</para>
        /// </summary>
        /// <param name="membership_id"></param>
        /// <returns></returns>
        public string Remove(string membership_id)
        {
            return Remove(this.group_id, membership_id);
        }

        /// <summary>
        /// Update your nickname in a group. The nickname must be between 1 and 50 characters.
        /// </summary>
        /// <param name="group_id"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public string Update(string group_id, string nickname)
        {
            string actionUrl = string.Format("/groups/{0}/memberships/update", group_id);

            string postData = "{\"membership\":{\"nickname\": \"" + nickname + "\"}}";

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json;charset=UTF-8";
                return client.UploadString(Constants.GetApiUrl(actionUrl), "POST", postData);
            }
        }

        /// <summary>
        /// This requires you use the constructor Members(string group_id)
        /// <para>See Update(string group_id, string nickname) for details</para>
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public string Update(string nickname)
        {
            return Update(this.group_id, nickname);
        }
    }

    /// <summary>
    /// Member class to be used inside the Members Api
    /// <para>nickname is Required. You must use one of the following identifiers: user_id, phone_number, or email.</para>
    /// </summary>
    public class Member
    {
        public string nickname { get; set; }
        public string user_id { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string guid { get; set; }

        public Member()
        {
            this.guid = Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
