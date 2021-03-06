﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core.Config
{
  public class MasterConfig
  {
    public ConnectionStringsConfig ConnectionStringConfig { get; set; }
    public AzureStorageConfig AzureStorageConfig { get; set; }
    public Auth0Config Auth0Config { get; set; }

    public StripeConfig StripeConfig { get; set; }
    public PayPalConfig PayPalConfig { get; set; }
  }
}
