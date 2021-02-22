using Fistix.Training.Domain.Commands.Stripe;
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


    public async void CheckoutSample(string email, long amount, string productName)
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var response = await _httpClient.PostAsync($"api/Stripe/CheckoutSample?email={email}&amount={amount}&productName={productName}", null);
        if (response.IsSuccessStatusCode)
        {
          var commandResult = await response.Content.ReadFromJsonAsync<CreateSessionCommandResult>();

          //var tasks = new List<TaskDto>(_tasksSubject.Value);
          //tasks.Add(commandResult.Payload);

          //_tasksSubject.OnNext(tasks);

          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = true,
            Operation = "CreateCheckoutSampleSession",
            Data = commandResult.SessionId
          });

        }
      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "CreateCheckoutSampleSession",
          ErrorMessage = ex.Message
        });
      }
    }

    public async Task CreateCustomer(CreateCustomerCommand command)
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var response = await _httpClient.PostAsJsonAsync<CreateCustomerCommand>("api/Stripe", command);
        if (response.IsSuccessStatusCode)
        {
          var commandResult = await response.Content.ReadFromJsonAsync<CreateCustomerCommandResult>();

          //var tasks = new List<TaskDto>(_tasksSubject.Value);
          //tasks.Add(commandResult.Payload);

          //_tasksSubject.OnNext(tasks);

          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = true,
            Operation = "CreateOrGetStripeCustomer",
            Data= commandResult.Id/*.Payload.Id.ToString()*/
          });

        }
      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "CreateOrGetStripeCustomer",
          ErrorMessage = ex.Message
        });
      }
    }


    public async Task OffSessionPayment(string customerId, long amount)
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var response = await _httpClient.PostAsync($"api/Stripe/OffSessionPayment?customerId={customerId}&amount={amount}", null);
        if (response.IsSuccessStatusCode)
        {
          //var commandResult = await response.Content.ReadFromJsonAsync<CreateCustomerCommandResult>();

          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = true,
            Operation = "OffSessionPayment",
            //Data = commandResult.CustomerId
          });

        }
      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "OffSessionPayment",
          ErrorMessage = ex.Message
        });
      }
    }

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
