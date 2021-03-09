using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class Link
  {
    public string href { get; set; }
    public string rel { get; set; }
    public string method { get; set; }

    //Additional property for GetSubscriptionPlansListModel
    public string encType { get; set; }
  }
}
