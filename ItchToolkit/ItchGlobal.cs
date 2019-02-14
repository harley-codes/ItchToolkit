using System.Net.Http;

namespace ItchToolkit
{
    class ItchGlobal
    {
        public static readonly HttpClient httpClient = new HttpClient();

        public static async System.Threading.Tasks.Task<string> GetResposeHTTPAsync(string URI)
        {
            return await httpClient.GetStringAsync(URI).ConfigureAwait(false);
        }

        public static async System.Threading.Tasks.Task<byte[]> GetResposeHTTPAsyncAsByteArray(string URI)
        {
            return await httpClient.GetByteArrayAsync(URI).ConfigureAwait(false);
        }
    }
}
