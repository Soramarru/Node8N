using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace n8nClient.Modeles
{
    public class Pipeline(string pipelineName, int idOwner, List<NodeTask> taskList, string mongoId , string? pipelineDesc)
    {
        [JsonProperty("_id")] public string MongoId { get; } = mongoId;
        [JsonProperty("pipelineName")] public string PipelineName { get; } = pipelineName;
        [JsonProperty("idOwner")] public int IdOwner { get; } = idOwner;
        [JsonProperty("taskList")] public List<NodeTask> TaskList { get; } = taskList;
        [JsonProperty("pipelineDesc")] public string? PipelineDesc { get; } = pipelineDesc;
    }
}
