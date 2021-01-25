using Fistix.Training.Domain.Commands.Tasks;
using Fistix.Training.Domain.Dtos;
using Fistix.Training.Domain.Queries.Tasks;
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
  public class TaskStateService
  {
    private readonly HttpClient _httpClient = null;
    private readonly AuthHandler _authHandler = null;

    public TaskStateService(HttpClient httpClient, AuthHandler authHandler)
    {
      _httpClient = httpClient;
      _authHandler = authHandler;

      GetAllTasks();
    }


    private BehaviorSubject<List<TaskDto>> _tasksSubject = new BehaviorSubject<List<TaskDto>>(new List<TaskDto>());
    private Subject<ApiCallResult> _apiCallResultSubject = new Subject<ApiCallResult>();
    public IObservable<List<TaskDto>> TaskObservable { get { return _tasksSubject; } }
    public IObservable<ApiCallResult> ApiCallResultObservable { get { return _apiCallResultSubject; } }


    public async void GetAllTasks()
    {
      await GetAll();
    }
    public async void CreateTask(CreateTaskCommand command)
    {
      await Create(command);
    }
    public async void UpdateTask(Guid id, UpdateTaskCommand command)
    {
      await Update(id, command);
    }

    private async Task GetAll()
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var result = await _httpClient.GetFromJsonAsync<GetAllTasksQueryResult>("api/tasks");

        _tasksSubject.OnNext(result.Payload);

        _apiCallResultSubject.OnNext(new ApiCallResult()
        {
          IsSucceed = true,
          Operation = "GetAllTasks"
        });

      }
      catch (Exception ex)
      {

        _apiCallResultSubject.OnNext(new ApiCallResult()
        {
          IsSucceed = false,
          Operation = "GetAllTasks",
          ErrorMessage = ex.Message
        });
      }

    }

    private async Task Create(CreateTaskCommand command)
    {
      try
      {

        var response = await _httpClient.PostAsJsonAsync<CreateTaskCommand>("api/Tasks", command);
        if (response.IsSuccessStatusCode)
        {
          var commandResult = await response.Content.ReadFromJsonAsync<CreateTaskCommandResult>();

          var tasks = new List<TaskDto>(_tasksSubject.Value);
          tasks.Add(commandResult.Payload);

          _tasksSubject.OnNext(tasks);
          _apiCallResultSubject.OnNext(new ApiCallResult()
          {
            IsSucceed = true,
            Operation = "CreateTask"
          });

        }
      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult()
        {
          IsSucceed = false,
          Operation = "CreateTask",
          ErrorMessage = ex.Message
        });
      }
    }

    private async Task Update(Guid id, UpdateTaskCommand command)
    {
      try
      {

        var response = await _httpClient.PostAsJsonAsync<UpdateTaskCommand>("api/Tasks", command);
        if (response.IsSuccessStatusCode)
        {


          var commandResult = await response.Content.ReadFromJsonAsync<UpdateTaskCommandResult>();


          var tasks = new List<TaskDto>(_tasksSubject.Value);

          tasks.Remove(tasks.SingleOrDefault(t => t.TaskId.Equals(id)));
          tasks.Add(commandResult.Payload);

          _tasksSubject.OnNext(tasks);
          _apiCallResultSubject.OnNext(new ApiCallResult()
          {
            IsSucceed = true,
            Operation = "UpdateTask"
          });

        }
      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult()
        {
          IsSucceed = false,
          Operation = "UpdateTask",
          ErrorMessage = ex.Message
        });
      }
    }

    private async Task<TaskDto> GetTaskById(Guid taskId)
    {
      var task = _tasksSubject.Value.SingleOrDefault(t => t.TaskId.Equals(taskId));
      return task;


      //TaskDto task = null;
      //if (_tasksSubject != null)
      //{
      //  task = _tasksSubject.Value.SingleOrDefault(t => t.TaskId.Equals(taskId));
      //}
      //else
      //{
      //  await GetToken();

      //  var response = await _http.GetAsync($"https://localhost:5001/api/tasks/{taskId}");
      //  if (response.IsSuccessStatusCode)
      //  {
      //    string content = await response.Content.ReadAsStringAsync();
      //    task = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModelForSinglePayload>(content).Payload;
      //  }
      //}

      //return task;
    }

  }
}
