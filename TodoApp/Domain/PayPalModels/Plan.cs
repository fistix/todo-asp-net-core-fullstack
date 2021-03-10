using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class Plan
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }

    [JsonProperty("usage_type")]
    public string UsageType { get; set; }

    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }
    public List<Link> Links { get; set; }
  }
}
