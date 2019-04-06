using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.Filters.Extensions;

namespace Swashbuckle.AspNetCore.Filters
{
    internal class ResponseExample
    {
        private readonly JsonFormatter jsonFormatter;
        private readonly SerializerSettingsDuplicator serializerSettingsDuplicator;

        public ResponseExample(
            JsonFormatter jsonFormatter,
            SerializerSettingsDuplicator serializerSettingsDuplicator)
        {
            this.jsonFormatter = jsonFormatter;
            this.serializerSettingsDuplicator = serializerSettingsDuplicator;
        }

        public void SetResponseExampleForStatusCode(
            OpenApiOperation operation,
            int statusCode,
            object example,
            IContractResolver contractResolver = null,
            JsonConverter jsonConverter = null)
        {
            if (example == null)
            {
                return;
            }

            var response = operation.Responses.FirstOrDefault(r => r.Key == statusCode.ToString());

            if (response.Equals(default(KeyValuePair<string, OpenApiResponse>)) == false && response.Value != null)
            {
                var serializerSettings = serializerSettingsDuplicator.SerializerSettings(contractResolver, jsonConverter);
                var jsonExample = jsonFormatter.FormatJson(example, serializerSettings, includeMediaType: true);
                foreach (var content in response.Value.Content)
                {
                    if (content.Key.Contains("xml"))
                    {
                        // todo, figure this out
                        //var xml = example.XmlSerialize();
                        //content.Value.Example = new OpenApiString(xml);
                    }
                    else
                    {
                        content.Value.Example = new OpenApiString(jsonExample);
                    }
                }
            }
        }
    }
}
