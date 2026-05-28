using n8nClient.Modeles;
using n8nClient.Requesters;
using System;
using System.Collections.Generic;

namespace n8nClient.Controllers
{
    internal class AuthController
    {
        /// <summary>
        /// Attempt a login request to the api and notify the user if it fails else login it and redirect him
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<ApiResult> AttemptLogin(string username , string password)
        {
            ApiResult res = await AuthRequests.Login(username, password);
            if (res.Status == "error")
                return res;
            if (res.Data.token != null)
            {
                Globals.token = res.Data.token;
                RoutesController.RouteTo("pipeView");
                return res;
            }
            return new ApiResult("error", "An error occured on our side please try again later." , "");

        }

        /// <summary>
        /// Attempt a register request to the api and notify the user if it fails else register it and redirect him
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<ApiResult> AttemptRegister(string username, string password)
        {
            ApiResult res = await AuthRequests.Register(username, password);
            if (res.Status == "error")
                return res;
            if (res.Data.token != null)
            {
                Globals.token = res.Data.token;
                RoutesController.RouteTo("pipeView");
                return res;
            }
            return new ApiResult("error", "An error occured on our side please try again later.", "");
        }
    }
}
