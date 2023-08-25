using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Framework.Extensions
{
    public static class HttpClientExtension
    {
        public static async Task<TResponse?> PostAsync<TResponse>(this HttpClient client, string url, object data, Dictionary<string, string> headers = null!, string mediaType = "application/json")
        {
            try
            {
                var response = await client
                    .AddMediaType(mediaType)
                    .AddHeaders(headers)
                    .PostAsync(url, new StringContent(data.Serialize(), Encoding.UTF8, mediaType));
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent.Deserialize<TResponse>();
                }
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
            client.DefaultRequestHeaders.Add("Authorization", accessToken);
            return client;
        }

        public static HttpClient AddHeaders(this HttpClient client, Dictionary<string, string>? headers)
        {
            if (headers == null) return client;
            foreach (var header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            return client;
        }

        public static HttpClient AddMediaType(this HttpClient client, string mediaType = "application/json")
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            return client;
        }
    }
}
