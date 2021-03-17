using Fistix.Training.Domain.PayPalModels;
using Fistix.Training.Domain.Queries.PayPal;
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
  public class PayPalStateService
  {

    private readonly HttpClient _httpClient = null;
    private readonly AuthHandler _authHandler = null;

    public PayPalStateService(HttpClient httpClient, AuthHandler authHandler)
    {
      _httpClient = httpClient;
      _authHandler = authHandler;
      GetAllPlans();
    }
    //private BehaviorSubject<List<TaskDto>> _tasksSubject = new BehaviorSubject<List<TaskDto>>(new List<TaskDto>());
    private BehaviorSubject<List<Plan>> _plansSubject = new BehaviorSubject<List<Plan>>(new List<Plan>());
    public IObservable<List<Plan>> PlansObservable { get { return _plansSubject; } }


    private Subject<ApiCallResult<string>> _apiCallResultSubject = new Subject<ApiCallResult<string>>();
    public IObservable<ApiCallResult<string>> ApiCallResultObservable { get { return _apiCallResultSubject; } }

    public async Task GetAllPlans()
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var result = await _httpClient.GetFromJsonAsync<GetAllSubscriptionPlansQueryResult>("api/PayPal/GetAllSubscriptionPlans");

        _plansSubject.OnNext(result.Plans);

        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = true,
          Operation = "GetAllPlans"
        });

      }

      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "GetAllPlans",
          ErrorMessage = ex.Message
        });
      }

    }
  }
}
