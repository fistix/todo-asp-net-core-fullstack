using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Shared.Models.Stripe
{
  public class OrderData
  {
    public List<Item> Items { get; set; }
    public string Currency { get; set; }
  }
}
