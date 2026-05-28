using n8nClient.Modeles;
using n8nClient.Requesters;
using n8nClient.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace n8nClient.Controllers
{
    internal class PipeViewController 
    {
        /// <summary>
        /// Get the username of the user, this is also used to ensure if the token is still valid otherwise redirect it
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Request the api all the pipelines to show them on the form
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Pipeline>> GetPipeList()
        {
            ApiResult res = await PipelineRequests.GetAllPipelines();
            return JsonConvert.DeserializeObject<List<Pipeline>>(res.Data.pipes.ToString());
        }


        /// <summary>
        /// Handle the forms to create a pipeline to the api and return the mongoID if successfull
        /// </summary>
        /// <returns></returns>
        public static async Task<string> HandlePipeCreation()
        {
            MultiInputForm pipeNameForm = new MultiInputForm(new ValidationConstraint("-1", "Create new pipeline", "", "Input a name for the pipeline", true, "string"));
            DialogResult pipeNameRes = pipeNameForm.ShowDialog();
            MultiInputForm pipeDescForm = new MultiInputForm(new ValidationConstraint("-1", "Describe the new pipeline", "", "Input a simple description", false, "string"));
            DialogResult pipeDescRes = pipeDescForm.ShowDialog();
            MultiInputForm pipeCronForm = new MultiInputForm(new ValidationConstraint("-1", "Schedule the pipeline (optional)", "", "ex: 0 9 * * * ", false, "string"));
            DialogResult pipeCronRes = pipeCronForm.ShowDialog();
            if (pipeNameRes == DialogResult.OK)
            {
                string name = pipeNameForm.result ?? "";
                string desc = pipeDescForm.result ?? "";
                string cron = pipeCronForm.result ?? "";
                ApiResult res = await PipelineRequests.CreateNewPipeline(name, desc, cron);
                MessageBox.Show(res.Message);
                return res.Data.id;
            }
            else
            {
                return "";
            }
        }

        
    }
}   
