using System.Collections.Generic;

namespace ItchToolkit
{
    /// <summary>
    /// Data about all the games you've uploaded or have edit access to.
    /// </summary>
    public class DeveloperGamesRequest
    {
        /// <summary>
        /// Request types compatible with my-games API Call.
        /// </summary>
        public class RequestType
        {
            /// <summary>
            /// Detailed information on a game.
            /// </summary>
            public class Game
            {
                /// <summary>
                /// Imaged used to represent your game.
                /// </summary>
                public string gameCoverURL;
                /// <summary>
                /// Date game was created in Itch.io
                /// </summary>
                public string createdDate;
                /// <summary>
                /// How many downloads your game has.
                /// </summary>
                public int downloadCount;
                /// <summary>
                /// The game ID of your game, used for API calls.
                /// </summary>
                public int gameID;
                /// <summary>
                /// The minimum purchase price for your game.
                /// </summary>
                public int minPrice;
                /// <summary>
                /// If the game supports Android.
                /// </summary>
                public bool supportsAndroid;
                /// <summary>
                /// If the game supports Linux.
                /// </summary>
                public bool supportsLinux;
                /// <summary>
                /// If the game supports Mac OSX.
                /// </summary>
                public bool supportsOSX;
                /// <summary>
                /// If the game supports Windows.
                /// </summary>
                public bool supportsWindows;
                /// <summary>
                /// If the game has been published.
                /// </summary>
                public bool isPublished;
                /// <summary>
                /// Date game was published.
                /// </summary>
                public string publishedDate;
                /// <summary>
                /// How many times your game has been purchased.
                /// <para></para>
                /// Use <see cref="downloadCount"/> to view how many times game has been downloaded.
                /// </summary>
                public int purchaseCount;
                /// <summary>
                /// Description added to game when publishing on Itch.io
                /// </summary>
                public string gameDescription;
                /// <summary>
                /// The title of your game when publishing on Itch.io
                /// </summary>
                public string gameTitile;
                /// <summary>
                /// Publish type.
                /// </summary>
                public string type;
                /// <summary>
                /// HTTP address for your game on Itch.io
                /// </summary>
                public string gameURL;
                /// <summary>
                /// How many people have viewed your game.
                /// </summary>
                public int viewCount;
                /// <summary>
                /// List of purchase summaries for each purchase this game has.
                /// </summary>
                public List<Earning> earnings = new List<Earning>();
                /// <summary>
                /// A summary for each purchase this game has.
                /// </summary>
                public class Earning
                {
                    /// <summary>
                    /// Currency Type.
                    /// <para></para>
                    /// Example: USD
                    /// </summary>
                    public string currency;
                    /// <summary>
                    /// Purchase amount in string format.
                    /// <para></para>
                    /// Example: $50.47
                    /// </summary>
                    public string purchaseAmountString;
                    /// <summary>
                    /// Purchase amount in base format.
                    /// <para></para>
                    /// Example: 5047
                    /// <para></para>
                    /// Which equals $50.47
                    /// </summary>
                    public int purchaseAmountInt;
                }
            }
            /// <summary>
            /// Error types that can be returned for invalid API calls.
            /// </summary>
            public enum ErrorType
            {
                /// <summary>
                /// {"errors":["invalid key"]}
                /// </summary>
                InvalidKey,
                /// <summary>
                /// {"errors":["api key does not permit `profile:games`"]}
                /// </summary>
                OutOfScopeKey,
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
        /// Detailed information on all games returned in API call.
        /// </summary>
        public List<RequestType.Game> games;
        /// <summary>
        /// Error returned for invalid API call.
        /// </summary>
        public RequestType.ErrorType requestError = RequestType.ErrorType.NULL;
        /// <summary>
        /// Fetches data about all the games you've uploaded or have edit access to.
        /// </summary>
        public DeveloperGamesRequest(string yourKey)
        {
            if (string.IsNullOrEmpty(yourKey)) { requestError = RequestType.ErrorType.DetailsMissing; return; }
            string requestURL = "https://itch.io/api/1/" + yourKey + "/my-games";
            string jsonResponse = ItchGlobal.GetResposeHTTPAsync(requestURL).GetAwaiter().GetResult();
            switch (jsonResponse)
            {
                case "{\"errors\":[\"invalid key\"]}":
                    requestError = RequestType.ErrorType.InvalidKey;
                    break;
                    case "{\"errors\":[\"api key does not permit `profile: games`\"]}":
                    requestError = RequestType.ErrorType.OutOfScopeKey;
                    break;
                default:
                    games = new List<RequestType.Game>();
                    var jsonObject = System.Json.JsonValue.Parse(jsonResponse);
                    for (int i = 0; i < jsonObject["games"].Count; i++)
                    {
                        RequestType.Game game = new RequestType.Game();
                        game.earnings = new List<RequestType.Game.Earning>();
                        game.gameCoverURL = jsonObject["games"][i].ContainsKey("cover_url") ? (string)jsonObject["games"][i]["cover_url"] : "";
                        game.createdDate = jsonObject["games"][i].ContainsKey("created_at") ? (string)jsonObject["games"][i]["created_at"] : "";
                        game.downloadCount = jsonObject["games"][i].ContainsKey("downloads_count") ? (int)jsonObject["games"][i]["downloads_count"] : 0;
                        game.gameID = jsonObject["games"][i].ContainsKey("id") ? (int)jsonObject["games"][i]["id"] : 0;
                        game.minPrice = jsonObject["games"][i].ContainsKey("min_price") ? (int)jsonObject["games"][i]["min_price"] : 0;
                        game.supportsAndroid = jsonObject["games"][i].ContainsKey("p_android") ? (bool)jsonObject["games"][i]["p_android"] : false;
                        game.supportsLinux = jsonObject["games"][i].ContainsKey("p_linux") ? (bool)jsonObject["games"][i]["p_linux"] : false;
                        game.supportsOSX = jsonObject["games"][i].ContainsKey("p_osx") ? (bool)jsonObject["games"][i]["p_osx"] : false;
                        game.supportsWindows = jsonObject["games"][i].ContainsKey("p_windows") ? (bool)jsonObject["games"][i]["p_windows"] : false;
                        game.isPublished = jsonObject["games"][i].ContainsKey("published") ? (bool)jsonObject["games"][i]["published"] : false;
                        game.publishedDate = jsonObject["games"][i].ContainsKey("published_at") ? (string)jsonObject["games"][i]["published_at"] : "";
                        game.purchaseCount = jsonObject["games"][i].ContainsKey("purchases_count") ? (int)jsonObject["games"][i]["purchases_count"] : 0;
                        game.gameDescription = jsonObject["games"][i].ContainsKey("short_text") ? (string)jsonObject["games"][i]["short_text"] : "";
                        game.gameTitile = jsonObject["games"][i].ContainsKey("title") ? (string)jsonObject["games"][i]["title"] : "";
                        game.type = jsonObject["games"][i].ContainsKey("type") ? (string)jsonObject["games"][i]["type"] : "";
                        game.gameURL = jsonObject["games"][i].ContainsKey("url") ? (string)jsonObject["games"][i]["url"] : "";
                        game.viewCount = jsonObject["games"][i].ContainsKey("views_count") ? (int)jsonObject["games"][i]["views_count"] : 0;
                        if(jsonObject["games"][i].ContainsKey("earnings"))
                            for (int ii = 0; ii < jsonObject["games"][i]["earnings"].Count; ii++)
                            {
                                RequestType.Game.Earning earning = new RequestType.Game.Earning();
                                earning.currency = jsonObject["games"][i]["earnings"][ii].ContainsKey("currency") ? (string)jsonObject["games"][i]["earnings"][ii]["currency"] : "";
                                earning.purchaseAmountString = jsonObject["games"][i]["earnings"][ii].ContainsKey("amount_formatted") ? (string)jsonObject["games"][i]["earnings"][ii]["amount_formatted"] : "";
                                earning.purchaseAmountInt = jsonObject["games"][i]["earnings"][ii].ContainsKey("amount") ? (int)jsonObject["games"][i]["earnings"][ii]["amount"] : 0;
                                game.earnings.Add(earning);
                            }
                        games.Add(game);
                    }
                    break;
            }
        }

        /// <summary>
        /// Searches your list of games based on search pram, returns Null if no search returns.
        /// </summary>
        /// <param name="gameTitleSearch"></param>
        /// <returns></returns>
        public string FindGameID(string gameTitleSearch)
        {
            if (games == null)
                return null;
            else
                foreach(RequestType.Game game in games)
                    if (!string.IsNullOrEmpty(game.gameTitile))
                        if (game.gameTitile.ToUpper().Contains(gameTitleSearch.ToUpper()))
                            return game.gameID.ToString();
            return null;
        }
    }
}
