using System;
using System.Collections.Generic;
using System.Text;

namespace Fistix.Training.Domain.PayPalModels
{
  public class Name
  {
    public string given_name { get; set; }
    public string surname { get; set; }
    public string full_name { get; set; }


    //Additional for OrderModel instead of Order default model of PayPal
    public string AlternateFullName { get; set; }
    public string FullName { get; set; }
    public string GivenName { get; set; }
    public string MiddleName { get; set; }
    public string Prefix { get; set; }
    public string Suffix { get; set; }
    //public string Surname;
  }
}
