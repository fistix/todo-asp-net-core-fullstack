using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class ProductModel
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public string Type { get; set; }
    public string Category { get; set; }

    [JsonProperty("create_time")]
    public string CreateTime { get; set; }

    [JsonProperty("update_time")]
    public string UpdateTime { get; set; }
  }
}
