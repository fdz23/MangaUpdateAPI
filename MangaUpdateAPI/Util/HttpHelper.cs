namespace MangaUpdateAPI.Util
{
    public static class HttpHelper
    {
        public static HttpRequestMessage GetValidRequest()
        {
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Head;
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.102 Safari/537.36");

            return request;
        }
    }
}
