using n8nClient.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace n8nClient.Controllers
{
    public class RoutesController
    {
        // The current shown form
        public static Form? runningForm;
        
        public static void RouteTo(string target)
        {
            Form? nextForm = (target.ToLower()) switch
            {
                "pipeview" => new PipeView(),
                "auth" => new AuthPage(),
                _ => throw new ArgumentException("Router tried to access an unknown routes : " + target)
            };

            // hide old form
            runningForm?.Hide();
            // gc and replace
            runningForm = nextForm;
            // exit the app if the cross button is clicked
            runningForm.FormClosed += (s, e) => Application.ExitThread(); 
            runningForm.Show();
        }
        public static void Exit()
        {
            Application.ExitThread();
        }

    }
}
