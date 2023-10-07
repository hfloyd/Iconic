using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Our.Iconic.Core.Models
{
    public class Package
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "selector")]
        public string? Selector { get; set; }

        [JsonProperty(PropertyName = "template")]
        public string? Template { get; set; }


        [JsonProperty(PropertyName = "cssfile")]
        public string? CssFile { get; set; }

        [JsonProperty(PropertyName = "sourcefile")]
        public string? SourceFile { get; set; }

        [JsonProperty(PropertyName = "extractedStyles")]
        public IEnumerable<string> ExtractedStyles { get; set; } = new List<string>();

        [JsonProperty(PropertyName = "filteredIcons")]
        public IEnumerable<string> FilteredIcons { get; set; } = new List<string>();
    }
}
