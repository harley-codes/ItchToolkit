namespace ItchToolkit
{
    /// <summary>
    /// Container for Itch API's Profile Response
    /// </summary>
    public class ItchUserRequest
    {
        /// <summary>
        /// Tpyes of information retrievable from ItchUserRequest
        /// </summary>
        public class RequestType
        {
            /// <summary>
            /// Container for information relevent to requested User
            /// </summary>
            public class User
            {
                /// <summary>
                /// URL address for avatar image
                /// </summary>
                public string avatarURL;
                /// <summary>
                /// Users preferred displa name
                /// </summary>
                public string displayName;
                /// <summary>
                /// If the user has previously published a game on Itch.io
                /// </summary>
                public bool isDeveloper;
                /// <summary>
                /// Unique ID relevent to User
                /// </summary>
                public int userID;
                /// <summary>
                /// URL address for user profile
                /// </summary>
                public string profileURL;
                /// <summary>
                /// If the user has previusly downloaded a game
                /// </summary>
                public bool isGamer;
                /// <summary>
                /// Username of the user
                /// </summary>
                public string userName;
                /// <summary>
                /// If the user is of press status
                /// </summary>
                public bool isPressUser;
            }
            /// <summary>
            /// Types of errors that can be returned from Itch.io API Profile:Me request
            /// </summary>
            public enum ErrorType
            {
                /// <summary>
                /// {"errors":["invalid key"]}
                /// </summary>
                InvalidKey,
                /// <summary>
                /// Custom error, for when the user has passed NULL values to the request.
                /// </summary>
                DetailsMissing,
                /// <summary>
                /// No Error returned
                /// </summary>
                NULL
            }
        }
        /// <summary>
        /// Container for information relevent to the requested Profile:Me
        /// </summary>
        public RequestType.User user;
        /// <summary>
        /// Error returned from Itch.io API Profile:Me request
        /// </summary>
        public RequestType.ErrorType requestError = RequestType.ErrorType.NULL;
        /// <summary>
        /// Create a new container for information relevent to requested User
        /// </summary>
        /// <param name="userKey"></param>
        public ItchUserRequest(string userKey)
        {
            if (string.IsNullOrEmpty(userKey)){ requestError = RequestType.ErrorType.DetailsMissing; return; }
            string requestURL = "https://itch.io/api/1/" + userKey + "/me";
            string jsonResponse = ItchGlobal.GetResposeHTTPAsync(requestURL).GetAwaiter().GetResult();
            if (jsonResponse == "{\"errors\":[\"invalid key\"]}")
                requestError = RequestType.ErrorType.InvalidKey;
            else
            {
                user = new RequestType.User();
                var jsonObject = System.Json.JsonValue.Parse(jsonResponse);
                user.avatarURL = jsonObject["user"]["cover_url"];
                user.displayName = jsonObject["user"]["display_name"];
                user.isDeveloper = jsonObject["user"]["developer"];
                user.userID = jsonObject["user"]["id"];
                user.profileURL = jsonObject["user"]["url"];
                user.isGamer = jsonObject["user"]["gamer"];
                user.userName = jsonObject["user"]["username"];
                user.isPressUser = jsonObject["user"]["press_user"];
            }
        }
        /// <summary>
        /// Returns False if any other codition apart from Owned is met.
        /// To get full request information use ItchToolkit.DownloadKeyRequest()
        /// </summary>
        /// <param name="yourKey"></param>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public bool IsGameOwned(string yourKey, string gameID)
        {
            if (string.IsNullOrEmpty(yourKey) || string.IsNullOrEmpty(gameID))
                return false;
            DownloadKeyRequest downloadKeyRequest = new DownloadKeyRequest(yourKey, gameID, user.userID.ToString());
            return (downloadKeyRequest.error == DownloadKeyRequest.RequestType.ErrorType.NULL);
        }
    }
}
