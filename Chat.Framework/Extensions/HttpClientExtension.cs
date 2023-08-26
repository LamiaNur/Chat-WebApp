using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Microsoft.Net.Http.Headers;

namespace Chat.Framework.Extensions
{
    public static class HttpClientExtension
    {
        public static async Task<TResponse?> PostAsync<TResponse>(this HttpClient client, string url, object data, Dictionary<string, string>? headers = null, string? mediaType = null, Encoding? encoding = null)
        {
            try
            {
                encoding ??= Encoding.UTF8;
                if (string.IsNullOrEmpty(mediaType)) mediaType = MediaTypeNames.Application.Json;
                var response = await client
                    .AddMediaType(mediaType)
                    .AddHeaders(headers)
                    .PostAsync(url, new StringContent(data.Serialize(), encoding, mediaType));
                if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode.ToString());
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent.Deserialize<TResponse>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return default;
        }

        public static HttpClient AddBearerToken(this HttpClient client, string accessToken)
        {
            if (!accessToken.StartsWith("Bearer "))
            {
                accessToken = "Bearer " + accessToken;
            }
            client.AddHeader(HeaderNames.Authorization, accessToken);
            return client;
        }

        public static HttpClient AddHeader(this HttpClient client, string key, string value)
        {
            client.DefaultRequestHeaders.Add(key, value);
            return client;
        }

        public static HttpClient AddHeaders(this HttpClient client, Dictionary<string, string>? headers)
        {
            if (headers == null) return client;
            foreach (var header in headers)
            {
                client.AddHeader(header.Key, header.Value);
            }
            return client;
        }

        public static HttpClient AddMediaType(this HttpClient client, string? mediaType)
        {
            if (string.IsNullOrEmpty(mediaType)) mediaType = MediaTypeNames.Application.Json;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            return client;
        }
    }
}
