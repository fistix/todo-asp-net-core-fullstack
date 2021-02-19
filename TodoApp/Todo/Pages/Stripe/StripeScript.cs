using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Todo.Shared.Models.Stripe;

namespace Todo.Pages.Stripe
{
  public class StripeScript
  {
    private readonly HttpClient _http = null;
    IJSRuntime _jsRuntime = null;
    public StripeScript(HttpClient http, IJSRuntime jsRuntime)
    {
      _http = http;
      _jsRuntime = jsRuntime;
    }

    public async Task<bool> FetchElements()
    {
      var orderData = new OrderData()
      {
        Items = new List<Item>()
        {
          new Item 
          {
            Id = "photo-subscription"
          }
        },
        Currency = "usd"
      };

      var response = await _http.PostAsJsonAsync("https://localhost:5001/api/Stripe/CreatePaymentIntent", orderData);
      if (response.IsSuccessStatusCode)
      {
        SetupElementResponse setupElementResponse = null;
        try
        {
          setupElementResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SetupElementResponse>(await _jsRuntime.InvokeAsync<string>("setupElements", await response.Content.ReadAsStringAsync()));

          var paymentIntentReponse = Newtonsoft.Json.JsonConvert.DeserializeObject<PaymentIntentReponse>(await response.Content.ReadAsStringAsync());

          await _jsRuntime.InvokeVoidAsync("fetchElements", paymentIntentReponse.PublicKey, setupElementResponse.Card.ToString(), setupElementResponse.ClientSecret);
        }
        catch(Exception ex)
        {
          //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(setupElementResponse));
          Console.WriteLine($"Error Occurred: {ex.Message}, StackTrace: {ex.StackTrace}");
        }
      }

      return true;
    }
  }


  public class PaymentIntentReponse
  {
    public string PublicKey { get; set; }
    public string ClientSecret { get; set; }
    public string Id { get; set; }
  }

}
