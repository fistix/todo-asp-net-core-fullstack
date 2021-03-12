﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class Address
  {
    [JsonProperty("address_line_1")]
    public string AddressLine1 { get; set; }
    
    [JsonProperty("address_line_2")]
    public string AddressLine2 { get; set; }
    
    [JsonProperty("admin_area_2")]
    public string AdminArea2 { get; set; }

    [JsonProperty("admin_area_1")]
    public string AdminArea1 { get; set; }

    [JsonProperty("postal_code")]
    public string PostalCode { get; set; }

    [JsonProperty("country_code")]
    public string CountryCode { get; set; }
  }
}
