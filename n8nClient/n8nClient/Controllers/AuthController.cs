using n8nClient.Modeles;
using n8nClient.Requesters;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace n8nClient.Controllers
{
    internal class AuthController : IController
    {
        // required IController func

        public static void ShowPage()
        {

        }

        public static void HidePage()
        {

        }
        // class specific code
        public static bool IsLoggedIn()
        {
            return false;
        }

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
