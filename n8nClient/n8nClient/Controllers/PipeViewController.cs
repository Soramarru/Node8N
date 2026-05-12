using n8nClient.Modeles;
using n8nClient.Requesters;
using System;
using System.Collections.Generic;
using System.Text;

namespace n8nClient.Controllers
{
    internal class PipeViewController : IController
    {
        public static void ShowPage()
        {

        }
        public static void HidePage()
        {

        }

        public static async Task<string> GetGreetings()
        {
            ApiResult res = await AuthRequests.CheckTokenValidity();
            if(res.Status == "error")
            {
                RoutesController.RouteTo("Auth");
                return "";
            }
            return res.Data.username;
        }

        public static void GetPipeList()
        {

        }

        
    }
}
