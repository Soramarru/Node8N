using n8nClient.Modeles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n8nClient.Requesters
{
    internal class Requesters
    {
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
                return JsonConvert.DeserializeObject<ApiResult>(response) ?? throw new Exception("Empty is invalid");
            }
            catch (Exception)
            {
                return new ApiResult( "error", "Server likely sent an invalid response." ,  "");
            }
        }
    }
}
