using Fistix.Training.Domain.Commands.Customers;
using Fistix.Training.Domain.Commands.Stripe;
using Fistix.Training.Domain.Queries.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Todo.Shared.Models;

namespace Todo.Shared.Services
{
  public class StripeStateService
  {
    private readonly HttpClient _httpClient = null;
    private readonly AuthHandler _authHandler = null;

    public StripeStateService(HttpClient httpClient, AuthHandler authHandler)
    {
      _httpClient = httpClient;
      _authHandler = authHandler;

      //Create();
      //CreateAndSave();
    }

    private Subject<ApiCallResult<string>> _apiCallResultSubject = new Subject<ApiCallResult<string>>();
    public IObservable<ApiCallResult<string>> ApiCallResultObservable { get { return _apiCallResultSubject; } }

    #region Existing

    public async Task GetCustomer(GetCustomerDetailByEmailQuery query)
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());

        var response = await _httpClient.GetFromJsonAsync<GetCustomerDetailByEmailQueryResult>($"api/Customers?email={query.Email}");
        if (response.Payload != null)
        {
          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = true,
            Operation = "GetStripeCustomerByEmail",
            Payload = response.Payload
          });
        }

        else
        {
          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = false,
            Operation = "GetStripeCustomerByEmail"
          });
        }
      }

      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "GetStripeCustomerByEmail",
          ErrorMessage = ex.Message
        });
      }
    }

    public async Task CreateCustomer(CreateCustomerCommand command)
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var response = await _httpClient.PostAsJsonAsync<CreateCustomerCommand>("api/Customers", command);
        if (response.IsSuccessStatusCode)
        {
          var commandResult = await response.Content.ReadFromJsonAsync<CreateCustomerCommandResult>();

          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = true,
            Operation = "CreateStripeCustomer",
            //Data = commandResult.Payload.StripeCustomerId,
            Payload = commandResult.Payload
          });
        }

        else
        {
          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = false,
            Operation = "CreateStripeCustomer",
            ErrorMessage = "Response is not succeeded from server."
          });
        }
      }

      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "CreateStripeCustomer",
          ErrorMessage = ex.Message
        });
      }
    }

    public async Task CheckoutSample(SampleCheckoutCommand command)
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var response = await _httpClient.PostAsJsonAsync<SampleCheckoutCommand>("api/Stripe/SampleCheckout", command);
        if (response.IsSuccessStatusCode)
        {
          var commandResult = await response.Content.ReadFromJsonAsync<SampleCheckoutCommandResult>();

          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = true,
            Operation = "CreateSampleCheckoutSession",
            Data = commandResult.SessionId
          });
        }

        else
        {
          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = false,
            Operation = "CreateSampleCheckoutSession",
            ErrorMessage = "Response is not succeeded from server."
          });
        }
      }

      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "CreateSampleCheckoutSession",
          ErrorMessage = ex.Message
        });
      }
    }

    public async Task PaymentDeduct(PaymentDeductCommand paymentDeductCommand)
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var response = await _httpClient.PostAsJsonAsync<PaymentDeductCommand>("api/Stripe/PaymentDeduct", paymentDeductCommand);
        if (response.IsSuccessStatusCode)
        {
          var commandResult = await response.Content.ReadFromJsonAsync<PaymentDeductCommandResult>();

          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = true,
            Operation = "PaymentDeduct",
          });
        }

        else
        {
          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = false,
            Operation = "PaymentDeduct",
            ErrorMessage = "Response is not succeeded from server."
          });
        }
      }

      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "PaymentDeduct",
          ErrorMessage = ex.Message
        });
      }
    }
    
    #endregion

    public async void CreateAndSave()
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var response = await _httpClient.GetAsync("api/Stripe/CreateAndSave");
        if (response.IsSuccessStatusCode)
        {
          var commandResult = await response.Content.ReadFromJsonAsync<SaveCustomerDetailsCommandResult>();

          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = true,
            Operation = "CreateAndSave",
            Data = commandResult.ClientSecret
          });

        }
        else
        {
          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = false,
            Operation = "CreateAndSave",
            ErrorMessage = "Response is not succeeded from server."
          });
        }
      }

      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "CreateAndSave",
          ErrorMessage = ex.Message
        });
      }
    }
  }
}
