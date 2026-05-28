using n8nClient.Modeles;
using System;
using System.Collections.Generic;
using System.Text;

namespace n8nClient.Requesters
{
    public class AuthRequests
    {
        public static async Task<ApiResult> Login(string username , string password)
        {
            string uri = Globals.API_URI + "/auth/login";
            return await Requesters.Post(uri, new { username, password });
        }

        public static async Task<ApiResult> Register(string username, string password) 
        {
            string uri = Globals.API_URI + "/auth/register";
            return await Requesters.Post(uri, new { username, password });
        }

        public static async Task<ApiResult> CheckTokenValidity()
        {
            string uri = Globals.API_URI + "/auth/validate";
            return await Requesters.Post(uri, new { Globals.token });
        }

    }
}
