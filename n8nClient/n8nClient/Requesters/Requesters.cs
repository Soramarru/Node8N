using n8nClient.Modeles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n8nClient.Requesters
{
    internal class Requesters
    {
        /// <summary>
        /// Helper request of type post,
        /// return a formatted ApiResult
        /// </summary>
        /// <param name="reqUri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<ApiResult> Post(string reqUri, object data)
        {
            using HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, reqUri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json")
            };

            try
            {
                HttpResponseMessage result = await httpClient.SendAsync(request);
                string response = await result.Content.ReadAsStringAsync();
                Console.WriteLine(response);
                return JsonConvert.DeserializeObject<ApiResult>(response) ?? throw new Exception("Empty is invalid");
            }
            catch (Exception)
            {
                return new ApiResult( "error", "Server likely sent an invalid response." ,  "");
            }
        }

        /// <summary>
        /// Helper request of type post,
        /// return a formatted ApiResult
        /// </summary>
        /// <param name="reqUri"></param>
        /// <returns></returns>
        public static async Task<string> get(string reqUri )
        {
            using HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, reqUri);
            try
            {
                HttpResponseMessage result = await httpClient.SendAsync(request);
                string response = await result.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception)
            {
                return ("");
            }
        }
    }
}
