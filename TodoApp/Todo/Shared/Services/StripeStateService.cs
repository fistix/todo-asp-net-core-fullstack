using Domain.Commands.Strip;
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

      Create();
    }

    private Subject<ApiCallResult<string>> _apiCallResultSubject = new Subject<ApiCallResult<string>>();
    public IObservable<ApiCallResult<string>> ApiCallResultObservable { get { return _apiCallResultSubject; } }


    public async void Create()
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var response = await _httpClient.PostAsync("api/Stripe",null);
        if (response.IsSuccessStatusCode)
        {
          var commandResult = await response.Content.ReadFromJsonAsync<CreateSessionCommandResult>();

          //var tasks = new List<TaskDto>(_tasksSubject.Value);
          //tasks.Add(commandResult.Payload);

          //_tasksSubject.OnNext(tasks);

          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = true,
            Operation = "CreateSession",
            Data = commandResult.SessionId
          });

        }
      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "CreateSession",
          ErrorMessage = ex.Message
        });
      }
    }
  }
}
