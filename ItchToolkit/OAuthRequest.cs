namespace ItchToolkit
{
    /// <summary>
    /// Manage OAuth Requests and Validation.
    /// </summary>
    public static class OAuthRequest
    {
        /// <summary>
        /// Launches a URL to a page that request an API key giving access to the users profile information.
        /// <para></para>
        /// After accepting, this key can be used to make further API calls.
        /// </summary>
        /// <param name="clientID"></param>
        public static void RequestOAuthApiKey(string clientID)
        {
            string url = "https://itch.io/user/oauth?client_id=" + clientID + "&scope=profile%3Ame&response_type=token&redirect_uri=urn%3Aietf%3Awg%3Aoauth%3A2.0%3Aoob";
            System.Diagnostics.Process.Start(url);
        }
        /// <summary>
        /// Verifies that the API Key given is valid for use.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsApiKeyValid(string key)
        {
            ItchUserRequest request = new ItchUserRequest(key);
            return (request.requestError == ItchUserRequest.RequestType.ErrorType.NULL);
        }
    }
}
