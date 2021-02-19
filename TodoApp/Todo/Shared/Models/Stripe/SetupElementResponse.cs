using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Shared.Models.Stripe
{
  public class SetupElementResponse
  {
    public string Id { get; set; }
    public string ClientSecret { get; set; }
    public JObject Stripe { get; set; }
    public JObject Card { get; set; }
  }
}
