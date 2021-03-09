using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class SubscriptionResponseModel
  {
    public string Id { get; set; }

    //[JsonPropertyName ("create_time")]
    [JsonProperty("create_time")]
    public string CreateTime { get; set; }

    //[JsonPropertyName("event_type")]
    [JsonProperty("event_type")]
    public string EventType { get; set; }


    //[JsonPropertyName("event_version")]
    [JsonProperty("event_version")]
    public string EventVersion { get; set; }


    //[JsonPropertyName("resource_type")]
    [JsonProperty("resource_type")]
    public string ResourceType { get; set; }

    public string Summary { get; set; }

    public Resource Resource { get; set; }
  }
}
