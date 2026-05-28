using n8nClient.Modeles;
using n8nClient.Requesters;
using n8nClient.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace n8nClient.Controllers
{
    internal class AddTaskController
    {
        /// <summary>
        /// Receive what the user want to add and to which pipeline, fetch the requirement json and do the job ( request the user for each input that the json ask ) 
        /// Then try to create the task on the pipeline, and notify user depending on success or failure using api result
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="jsonString"></param>
        /// <param name="pipeId"></param>
        /// <returns></returns>
        public static async Task<ApiResult?> addTaskToPipe(string mode , string jsonString , string pipeId)
        {
            NodeRequirements? req = null;
            if (mode == "nodeBlock")
                req = await RequirementRequests.GetNodeJsonReq(jsonString);
            else if (mode == "conditionalBlock")
                req = await RequirementRequests.GetLogicalJsonReq(jsonString);
            if (req == null)
                return new ApiResult("warning" , "The data provided can not be used ." , "");

            Dictionary<string, dynamic> collectedInputs = new Dictionary<string, dynamic>();

            foreach (ValidationConstraint constraint in req.ValidationFields)
            {
                MultiInputForm inputForm = new MultiInputForm(constraint);

                DialogResult dialogResult = inputForm.ShowDialog();

                if (dialogResult != DialogResult.OK)
                {
                    MessageBox.Show("User cancelled insertion.");
                    return null;
                }
                Console.WriteLine("Requested a " + constraint.WantedType + " But received a " + inputForm.result.GetType().ToString());
                collectedInputs[constraint.ObjId] = inputForm.result;

            }

            ApiResult result = await PipelineRequests.AddTaskToPipeline(
                taskName: req.Title,
                taskNodeName: jsonString,
                taskType: mode == "conditionalBlock" ? "logicalBlock" : "task",
                targetPipeId: pipeId,
                data: collectedInputs
            );
            return result;
        }
    }
}
