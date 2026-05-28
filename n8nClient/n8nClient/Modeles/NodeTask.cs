using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n8nClient.Modeles
{
    public class NodeTask(string taskType, string taskName, string taskNodeName, dynamic data)
    {
        [JsonProperty("taskType")] public string TaskType { get; } = taskType;
        [JsonProperty("taskName")] public string TaskName { get; } = taskName;
        [JsonProperty("taskNodeName")] public string TaskNodeName { get; } = taskNodeName;
        [JsonProperty("data")] public dynamic Data { get; } = data;

        public override string ToString()
        {
            return "Type : " + this.TaskType + "\nTask name : " + this.TaskName;
        }

    }
}
