using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n8nClient.Modeles
{
    internal class NodeRequirements(string title, string description, List<ValidationConstraint> validationFields)
    {
        [JsonProperty("title")] public string Title { get; } = title;
        [JsonProperty("description")] public string Description { get; } = description;
        [JsonProperty("validationFields")] public List<ValidationConstraint> ValidationFields { get; } = validationFields;
    }
}
