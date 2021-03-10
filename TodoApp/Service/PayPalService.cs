using Fistix.Training.Domain.PayPalModels;
using Fistix.Training.Domain.Queries.PayPal;
//
using Microsoft.AspNetCore.Mvc;
using PayPalCheckoutSdk.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Service
{
  public class PayPalService
  {
    private readonly HttpClient _httpClient = null;
    public PayPalService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public const string BearerToken = "A21AAJrjyXzsZG7JUsw8nTBVXAyyK8ZxfZNQhFR8qtMTlrHDm-eeulvnMdvmsajBSX-oKVJW029VUKR6gE0YHud5SeF0w51eA";

    #region OneTimeCheckout
    public async Task<Order> CreateOrder(bool debug = true)
    {

      var request = new OrdersCreateRequest();
      request.Prefer("return=representation");
      request.RequestBody(BuildRequestBody());
      //3. Call PayPal to set up a transaction
      var response = await PayPalClientService.Client().Execute(request);


      //if (debug)
      //{
      var result = response.Result<Order>();
      Console.WriteLine("Status: {0}", result.Status);
      Console.WriteLine("Order Id: {0}", result.Id);
      Console.WriteLine("Intent: {0}", result.CheckoutPaymentIntent/*Intent*/);
      Console.WriteLine("Links:");
      foreach (LinkDescription link in result.Links)
      {
        Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
      }
      PayPalCheckoutSdk.Orders.AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown/*Amount*/;
      Console.WriteLine("Total Amount: {0} {1}", amount.CurrencyCode, amount.Value);
      //}

      return result;
      //return Json(new
      //{
      //  id = result.Id,
      //  CheckoutPaymentIntent = result.CheckoutPaymentIntent,
      //  CreateTime = result.CreateTime,
      //  ExpirationTime = result.ExpirationTime,
      //  Links = result.Links,
      //  Payer = result.Payer,
      //  PurchaseUnits = result.PurchaseUnits,
      //  Status = result.Status,
      //  UpdateTime = result.UpdateTime
      //});

    }

    private static OrderRequest BuildRequestBody()
    {
      OrderRequest orderRequest = new OrderRequest()
      {
        CheckoutPaymentIntent = "CAPTURE",

        ApplicationContext = new ApplicationContext
        {
          BrandName = "EXAMPLE INC",
          LandingPage = "BILLING",
          UserAction = "CONTINUE",
          ShippingPreference = "SET_PROVIDED_ADDRESS"
        },
        PurchaseUnits = new List<PurchaseUnitRequest>
        {
          new PurchaseUnitRequest{
            ReferenceId =  "PUHF",
            Description = "Sporting Goods",
            CustomId = "CUST-HighFashions",
            SoftDescriptor = "HighFashions",
            AmountWithBreakdown = new PayPalCheckoutSdk.Orders.AmountWithBreakdown
            {
              CurrencyCode = "USD",
              Value = "230.00",
              AmountBreakdown = new AmountBreakdown
              {
                ItemTotal = new Money
                {
                  CurrencyCode = "USD",
                  Value = "180.00"
                },
                Shipping = new Money
                {
                  CurrencyCode = "USD",
                  Value = "30.00"
                },
                Handling = new Money
                {
                  CurrencyCode = "USD",
                  Value = "10.00"
                },
                TaxTotal = new Money
                {
                  CurrencyCode = "USD",
                  Value = "20.00"
                },
                ShippingDiscount = new Money
                {
                  CurrencyCode = "USD",
                  Value = "10.00"
                }
              }
            },
            Items = new List<PayPalCheckoutSdk.Orders.Item>
            {
              new PayPalCheckoutSdk.Orders.Item
              {
                Name = "T-shirt",
                Description = "Green XL",
                Sku = "sku01",
                UnitAmount = new Money
                {
                  CurrencyCode = "USD",
                  Value = "90.00"
                },
                Tax = new Money
                {
                  CurrencyCode = "USD",
                  Value = "10.00"
                },
                Quantity = "1",
                Category = "PHYSICAL_GOODS"
              },
              new PayPalCheckoutSdk.Orders.Item
              {
                Name = "Shoes",
                Description = "Running, Size 10.5",
                Sku = "sku02",
                UnitAmount = new Money
                {
                  CurrencyCode = "USD",
                  Value = "45.00"
                },
                Tax = new Money
                {
                  CurrencyCode = "USD",
                  Value = "5.00"
                },
                Quantity = "2",
                Category = "PHYSICAL_GOODS"
              }
            },
            ShippingDetail = new ShippingDetail
            {
              Name = new PayPalCheckoutSdk.Orders.Name
              {
                FullName = "John Doe"
              },
              AddressPortable = new AddressPortable
              {
                AddressLine1 = "123 Townsend St",
                AddressLine2 = "Floor 6",
                AdminArea2 = "San Francisco",
                AdminArea1 = "CA",
                PostalCode = "94107",
                CountryCode = "US"
              }
            }
          }
        }
      };

      return orderRequest;
    }

    public async Task<Order> CaptureOrder(string OrderId, bool debug = true)
    {
      var request = new OrdersCaptureRequest(OrderId);
      request.Prefer("return=representation");
      request.RequestBody(new OrderActionRequest());
      //3. Call PayPal to capture an order
      var response = await PayPalClientService.Client().Execute(request);
      //4. Save the capture ID to your database. Implement logic to save capture to your database for future reference.

      //if (debug)
      //{
      var result = response.Result<Order>();
      Console.WriteLine("Status: {0}", result.Status);
      Console.WriteLine("Order Id: {0}", result.Id);
      Console.WriteLine("Intent: {0}", result.CheckoutPaymentIntent);
      Console.WriteLine("Links:");
      foreach (LinkDescription link in result.Links)
      {
        Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
      }
      Console.WriteLine("Capture Ids: ");
      foreach (PurchaseUnit purchaseUnit in result.PurchaseUnits)
      {
        foreach (PayPalCheckoutSdk.Orders.Capture capture in purchaseUnit.Payments.Captures)
        {
          Console.WriteLine("\t {0}", capture.Id);
        }
      }
      PayPalCheckoutSdk.Orders.AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown;
      Console.WriteLine("Buyer:");
      Console.WriteLine("\tEmail Address: {0}\n\tName: {1}\n\tPhone Number: {2}", result.Payer.Email,
        result.Payer.Name.GivenName + " " + result.Payer.Name.Surname, result.Payer.AddressPortable.CountryCode
        );
      //}

      return result;
      //return Json(new
      //{
      //  id = result.Id,
      //  CheckoutPaymentIntent = result.CheckoutPaymentIntent,
      //  CreateTime = result.CreateTime,
      //  Links = result.Links,
      //  Payer = result.Payer,
      //  PurchaseUnits = result.PurchaseUnits,
      //  Status = result.Status,
      //  UpdateTime = result.UpdateTime,
      //  GivenName = result.Payer.Name.GivenName,
      //  SurName = result.Payer.Name.Surname,
      //});

    }

    #endregion

    #region API calls
    public async Task<SubscriptionPlanDetailModel> GetAllSubscriptionPlans()
    {
      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
      //var result = await _httpClient.GetFromJsonAsync("api/profiles");
      var response = await _httpClient.GetAsync("https://api-m.sandbox.paypal.com/v1/billing/plans");
      var contentString = await response.Content.ReadAsStringAsync();

      var content = Newtonsoft.Json.JsonConvert.DeserializeObject<SubscriptionPlanDetailModel>(contentString);

      return content;
      //return Ok();

    }


    public async Task<SubscriptionPlanDetailsByPlanIdModel> GetSubscriptionPlanDetailById(GetSubscriptionPlanDetailByIdQuery query)
    {

      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);

      var response = await _httpClient.GetAsync($"https://api-m.sandbox.paypal.com/v1/billing/plans/{query.Id}");
      var contentString = await response.Content.ReadAsStringAsync();

      var content = Newtonsoft.Json.JsonConvert.DeserializeObject<SubscriptionPlanDetailsByPlanIdModel>(contentString);

      //var result = await _httpClient.GetFromJsonAsync<SubscriptionPlanDetailsByPlanIdModel>($"https://api-m.sandbox.paypal.com/v1/billing/plans/{query.Id}");

      return content;
      //return Ok();
    }


    public async Task<GetAllTransactionsHistoryBySubscriptionIdModel> GetAllTransactionsDetailsBySubscriptionId(GetAllTransactionsHistoryBySubscriptionIdQuery query)
    {
      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);

      //var response = await _httpClient
      //  //.GetAsync($"https://api-m.sandbox.paypal.com/v1/billing/subscriptions/{query.Id}/transactions?start_time=2021-03-01T07:50:20.940Z&end_time=2021-03-09T07:50:20.940Z");
      //  .GetAsync($"https://api-m.sandbox.paypal.com/v1/billing/subscriptions/{query.Id}/transactions?start_time={query.StartTime}&end_time={query.EndTime}");

      //var contentString = await response.Content.ReadAsStringAsync();

      //var content = Newtonsoft.Json.JsonConvert.DeserializeObject(contentString);

      var result = await _httpClient.GetFromJsonAsync<GetAllTransactionsHistoryBySubscriptionIdModel>
        ($"https://api-m.sandbox.paypal.com/v1/billing/subscriptions/{query.Id}/transactions?start_time=2021-03-01T07:50:20.940Z&end_time=2021-03-09T07:50:20.940Z");

      return result;
      //return Ok();

    }


    #endregion

  }
}
