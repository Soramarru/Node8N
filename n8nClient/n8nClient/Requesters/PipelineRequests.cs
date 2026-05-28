using n8nClient.Modeles;
using System;
using System.Collections.Generic;
using System.Text;

namespace n8nClient.Requesters
{
    public class PipelineRequests
    {
        public static async Task<ApiResult> GetAllPipelines()
        {
            string uri = Globals.API_URI + "/pipe/get";
            return await Requesters.Post(uri, new { Globals.token });
        }
        public static async Task<ApiResult> CreateNewPipeline(string name, string desc = "", string cron = "")
        {
            string uri = Globals.API_URI + "/pipe/create";
            if (cron != "")
                return await Requesters.Post(uri, new { Globals.token, pipeName = name, pipelineDesc = desc, schedule = cron });
            return await Requesters.Post(uri, new { Globals.token, pipeName = name, pipelineDesc = desc });
        }
        public static async Task<ApiResult> DeletePipelineById(string mongoId)
        {
            string uri = Globals.API_URI + "/pipe/delete";
            return await Requesters.Post(uri, new { Globals.token, mongoId});
        }

        public static async Task<ApiResult> AddTaskToPipeline(string taskName, string taskNodeName, string taskType, string targetPipeId, Dictionary<string, dynamic> data)
        {
            string uri = Globals.API_URI + "/pipe/task/add";
            return await Requesters.Post(uri, new
            {
                Globals.token,
                taskType,
                taskName,
                taskNodeName,
                data,
                targetPipeId
            });
        }
    }
}
