using n8nClient.Modeles;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace n8nClient.Requesters
{
    internal class RequirementRequests
    {
        public static async Task<List<string>> GetReqNodesNameList()
        {
            string uri = Globals.API_URI + "/nodes/requirement/list";
            string res = await Requesters.get(uri);
            ApiResult result = JsonConvert.DeserializeObject<ApiResult>(res);
            return result.Data.dir.ToObject<List<string>>();
        }

        public static async Task<NodeRequirements> GetNodeJsonReq(string wantedJson)
        {
            string uri = Globals.API_URI + "/nodes/requirement/" + wantedJson;
            string res = await Requesters.get(uri);
            return JsonConvert.DeserializeObject<NodeRequirements>(res);
        }

        public static async Task<List<string>> GetReqLogicalNameList()
        {
            string uri = Globals.API_URI + "/logicals/requirement/list";
            string res = await Requesters.get(uri);
            ApiResult result = JsonConvert.DeserializeObject<ApiResult>(res);
            return result.Data.dir.ToObject<List<string>>();
        }

        public static async Task<NodeRequirements> GetLogicalJsonReq(string wantedJson)
        {
            string uri = Globals.API_URI + "/logicals/requirement/" + wantedJson;
            string res = await Requesters.get(uri);
            return JsonConvert.DeserializeObject<NodeRequirements>(res);
        }
    }
}
