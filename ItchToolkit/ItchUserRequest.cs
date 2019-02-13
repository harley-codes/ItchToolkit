namespace ItchToolkit
{
    public class ItchUserRequest
    {
        public class RequestType
        {
            public class User
            {
                public string avatarURL;
                public string displayName;
                public bool isDeveloper;
                public int userID;
                public string profileURL;
                public bool isGamer;
                public string userName;
                public bool isPressUser;
            }
            public enum ErrorType { InvalidKey, NULL }
        }
        
        public RequestType.User user = new RequestType.User();
        public RequestType.ErrorType requestError = RequestType.ErrorType.NULL;

        public ItchUserRequest(string userToken)
        {
            if (string.IsNullOrEmpty(userToken)){ requestError = RequestType.ErrorType.InvalidKey; return; }
            string requestURL = "https://itch.io/api/1/" + userToken + "/me";
            string jsonResponse = ItchGlobal.GetResposeHTTPAsync(requestURL).GetAwaiter().GetResult();
            if (jsonResponse == "{\"errors\":[\"invalid key\"]}")
                requestError = RequestType.ErrorType.InvalidKey;
            else
            {
                var jsonObject = System.Json.JsonValue.Parse(jsonResponse);
                user.avatarURL = jsonObject["user"]["cover_url"];
                user.displayName = jsonObject["user"]["display_name"];
                user.isDeveloper = jsonObject["user"]["developer"];
                user.userID = jsonObject["user"]["id"];
                user.profileURL = jsonObject["user"]["url"];
                user.isGamer = jsonObject["user"]["gamer"];
                user.userName = jsonObject["user"]["username"];
                user.isPressUser = jsonObject["user"]["press_user"];
                requestError = RequestType.ErrorType.NULL;
            }
        }
    }
}
