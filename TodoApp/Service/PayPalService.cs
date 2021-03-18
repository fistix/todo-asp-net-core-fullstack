using Fistix.Training.Domain.Commands.Paypal;
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

    public const string BearerToken = "A21AAKMi3FThuvvW_umR1541cdkzF8b5zwARNI9QzlAShYMIfcXC0yGkd1rm7LkwLOfa9qgCg3NRKreJeJ9xOB1q1PbPd0RAQ";

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
          //Check effects of these properties
          BrandName = "EXAMPLE INC",
          LandingPage = "BILLING",
          UserAction = "CONTINUE",

          ShippingPreference = "SET_PROVIDED_ADDRESS"
        },
        PurchaseUnits = new List<PurchaseUnitRequest>
        {
          new PurchaseUnitRequest{
            //Check effects of these properties
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
            Items = new List<Item>
            {
              new Item
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
              new Item
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
              AddressPortable = new PayPalCheckoutSdk.Orders.AddressPortable
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
        foreach (Capture capture in purchaseUnit.Payments.Captures)
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
    //public async Task<SubscriptionPlanDetailModel> GetAllSubscriptionPlans()
    //{
    //  _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
    //  //var result = await _httpClient.GetFromJsonAsync("api/profiles");
    //  var response = await _httpClient.GetAsync("https://api-m.sandbox.paypal.com/v1/billing/plans");
    //  var contentString = await response.Content.ReadAsStringAsync();

    //  var content = Newtonsoft.Json.JsonConvert.DeserializeObject<SubscriptionPlanDetailModel>(contentString);
      
    //  return content;
    //  //return Ok();

    //}

    public async Task<List<Plan>/*SubscriptionPlanDetailModel*/> GetAllSubscriptionPlans()
    {
      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
      //var result = await _httpClient.GetFromJsonAsync("api/profiles");
      var response = await _httpClient.GetAsync("https://api-m.sandbox.paypal.com/v1/billing/plans");
      var contentString = await response.Content.ReadAsStringAsync();

      var content = Newtonsoft.Json.JsonConvert.DeserializeObject</*List<Plan>*/SubscriptionPlanDetailModel>(contentString);
      List<Plan> plans = content.Plans.ToList();
      return plans;
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
        //($"https://api-m.sandbox.paypal.com/v1/billing/subscriptions/{query.Id}/transactions?start_time=2021-03-01T07:50:20.940Z&end_time=2021-03-09T07:50:20.940Z");
        ($"https://api-m.sandbox.paypal.com/v1/billing/subscriptions/{query.SubscriptionId}/transactions?start_time={query.StartTime}&end_time={query.EndTime}");

      return result;
      //return Ok();

    }


    public async Task<CreatePlanModel> CreatePlan(CreateSubscriptionPlanCommand command)
    {

      Frequency frequency = new Frequency()
      {
        interval_unit = "MONTH",
        interval_count = 1
      };

      FixedPrice fixedPrice = new FixedPrice()
      {
        value = "10",
        currency_code = "USD"
      };

      PricingScheme pricingScheme = new PricingScheme()
      {
        fixed_price = fixedPrice
      };
      //List
      //BillingCycle billing_cycles = new BillingCycle()
      //{
      //  frequency = frequency,
      //  tenure_type = "TRIAL",
      //  sequence = 1,
      //  total_cycles = 1
      //};
      List<BillingCycle> billing_cycles = new List<BillingCycle>()
      //BillingCycle billing_cycles = new BillingCycle()
      {
        new BillingCycle()
        {
          frequency = frequency,
          tenure_type = "TRIAL",
          sequence = 1,
          total_cycles = 1,
          //pricing_scheme = pricingScheme
        },

        new BillingCycle()
        {
          frequency = frequency,
          tenure_type = "REGULAR",
          sequence = 2,
          //total_cycles = 12,
          total_cycles = command.BillingCycles,
          pricing_scheme = pricingScheme
        }
      };

      Taxes taxes = new Taxes()
      {
        percentage = "10",
        inclusive = false
      };

      SetupFee setupFee = new SetupFee()
      {
        //value = "10",
        value = command.SubscriptionSetupFee,
        currency_code = "USD"
      };

      PaymentPreferences paymentPreferences = new PaymentPreferences()
      {
        auto_bill_outstanding = true,
        setup_fee = setupFee,
        setup_fee_failure_action = "CONTINUE",
        payment_failure_threshold = 3
      };

      CreatePlanModel createPlanModel = new CreatePlanModel()
      {
        ProductId = "product_social_marketing",
        //ProductId = command.ProductId,
        //Name = "Basic Plan",
        Name = command.PlanName,
        //Description = "Basic plan",
        Description = command.PlanDescription,
        BillingCycles= billing_cycles,
        PaymentPreferences = paymentPreferences,
        Taxes = taxes
      };

      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);

      var result = await _httpClient.PostAsJsonAsync<CreatePlanModel>("https://api-m.sandbox.paypal.com/v1/billing/plans", createPlanModel);

      //Working
      //var result = await _httpClient.PostAsync("https://api-m.sandbox.paypal.com/v1/billing/plans", new StringContent(json, Encoding.UTF8, "application/json"));
      //var json = System.Text.Json.JsonSerializer.Serialize(createPlanModel);



      //var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"https://api-m.sandbox.paypal.com/v1/billing/plans")
      //{
      //  Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(createPlanModel), System.Text.Encoding.UTF8, "application/json")
      //});

      //var result = await _httpClient.PostAsync($"https://api-m.sandbox.paypal.com/v1/billing/plans?{json}", new System.Net.Http.StringContent(json/*, Encoding.UTF8, "application/json"*/));
      //await _httpClient.PostAsync(_configuration["SisSendBulkSmsURL"], new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json"));
      //await _httpClient.PostAsync(, new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json"));
      //var responsee = await _httpClient.PostAsync($"https://api-m.sandbox.paypal.com/v1/billing/plans?product_id=product_social_marketing&name=BasicPlan&description=Basicplan&billing_cycles[0][frequency][interval_unit]=MONTH&billing_cycles[0][frequency][interval_count]=1&billing_cycles[0][tenure_type]=TRIAL&billing_cycles[0][sequence]=1&billing_cycles[0][total_cycles]=1&billing_cycles[1][frequency][interval_unit]=MONTH&billing_cycles[1][frequency][interval_count]=1&billing_cycles[1][tenure_type]=REGULAR&billing_cycles[1][sequence]=2&billing_cycles[1][total_cycles]=12&billing_cycles[1][pricing_scheme][fixed_price][value]=10&billing_cycles[1][pricing_scheme][fixed_price][currency_code]=USD&payment_preferences[auto_bill_outstanding]=true&payment_preferences[setup_fee][value]=10&payment_preferences[setup_fee][currency_code]=USD&payment_preferences[setup_fee_failure_action]=CONTINUE&payment_preferences[payment_failure_threshold]=3&taxes[percentage]=10&taxes[inclusive]=false",null);

      if (result.IsSuccessStatusCode)
      {
        string content = await result.Content.ReadAsStringAsync();
        //All properties are getting null except name and description
        createPlanModel = Newtonsoft.Json.JsonConvert.DeserializeObject<CreatePlanModel>(content);
        //objects are getting null, properties are filled
        createPlanModel = await result.Content.ReadFromJsonAsync<CreatePlanModel>();

      }

      else
      {
        string content = await result.Content.ReadAsStringAsync();
      }

      return createPlanModel;

    }
    
    
    public async Task<SubscriptionDetailByIdResponseModel> GetSubscriptionDetailbyId(string subscriptionId)
    {
      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);

      //var response = await _httpClient.GetAsync($"https://api-m.sandbox.paypal.com/v1/billing/subscriptions/{subscriptionId}");
      //if (response.IsSuccessStatusCode)
      //{
      //  string content = await response.Content.ReadAsStringAsync();
      //  //All properties are getting null except name and description
      //  var createPlanModel = Newtonsoft.Json.JsonConvert.DeserializeObject<SubscriptionDetailByIdResponseModel>(content);
      //  //objects are getting null, properties are filled
      //  createPlanModel = await response.Content.ReadFromJsonAsync<SubscriptionDetailByIdResponseModel>();

      //}


      var result = await _httpClient.GetFromJsonAsync<SubscriptionDetailByIdResponseModel>
         ($"https://api-m.sandbox.paypal.com/v1/billing/subscriptions/{subscriptionId}");

      return result;
    
    }
    #endregion

  }
}
