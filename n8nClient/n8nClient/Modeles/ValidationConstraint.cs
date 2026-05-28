using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n8nClient.Modeles
{
    public class ValidationConstraint(string objId, string title, string labelText, string placeholder, bool required, string wantedType)
    {
        [JsonProperty("objId")] public string ObjId { get; } = objId;
        [JsonProperty("title")] public string Title { get; } = title;
        [JsonProperty("labelText")] public string LabelText { get; } = labelText;
        [JsonProperty("placeholder")] public string Placeholder { get; } = placeholder;
        [JsonProperty("required")] public bool Required { get; } = required;
        [JsonProperty("wantedType")] public string WantedType { get; } = wantedType;
    }
}
