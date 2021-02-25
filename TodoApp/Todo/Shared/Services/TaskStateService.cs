using Fistix.Training.Domain.Commands.Tasks;
using Fistix.Training.Domain.Dtos;
using Fistix.Training.Domain.Queries.Profiles;
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
    private Subject<ApiCallResult<string>> _apiCallResultSubject = new Subject<ApiCallResult<string>>();

    public IObservable<List<TaskDto>> TaskObservable { get { return _tasksSubject; } }
    public IObservable<ApiCallResult<string>> ApiCallResultObservable { get { return _apiCallResultSubject; } }
    

    public async Task<TaskDto> GetTaskById(Guid id)
    {
      var task = await GetById(id);
      return task;
    }
  
 

    public async Task CreateTask(CreateTaskCommand command)
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var response = await _httpClient.PostAsJsonAsync<CreateTaskCommand>("api/Tasks", command);
        if (response.IsSuccessStatusCode)
        {
          var commandResult = await response.Content.ReadFromJsonAsync<CreateTaskCommandResult>();

          var tasks = new List<TaskDto>(_tasksSubject.Value);
          tasks.Add(commandResult.Payload);

          _tasksSubject.OnNext(tasks);

          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = true,
            Operation = "CreateTask"
          });

        }
      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "CreateTask",
          ErrorMessage = ex.Message
        });
      }
    }
    public async Task UpdateTask(Guid id, UpdateTaskCommand command)
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());

        //For replacing the object
        var task = await GetById(id);

        var response = await _httpClient.PutAsJsonAsync<UpdateTaskCommand>($"api/Tasks/{id}", command);
        if (response.IsSuccessStatusCode)
        {
          var commandResult = await response.Content.ReadFromJsonAsync<UpdateTaskCommandResult>();

          var tasks = new List<TaskDto>(_tasksSubject.Value);

          //For replacing the object
          var idx = tasks.IndexOf(task);
          if (idx >= 0)
          {
            tasks[idx] = commandResult.Payload;
          }

          //tasks.Remove(tasks.SingleOrDefault(t => t.TaskId.Equals(id)));
          //tasks.Add(commandResult.Payload);

          _tasksSubject.OnNext(tasks);
          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = true,
            Operation = "UpdateTask"
          });

        }
      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "UpdateTask",
          ErrorMessage = ex.Message
        });
      }
    }
    public async Task DeleteTask(Guid id)
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());

        var response = await _httpClient.DeleteAsync($"api/Tasks/{id}");
        if (response.IsSuccessStatusCode)
        {
          var tasks = new List<TaskDto>(_tasksSubject.Value);

          tasks.Remove(tasks.SingleOrDefault(t => t.Id.Equals(id)));

          _tasksSubject.OnNext(tasks);
          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = true,
            Operation = "DeleteTask"
          });

        }
      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "DeleteTask",
          ErrorMessage = ex.Message
        });
      }
    }
    private async Task<TaskDto> GetById(Guid id)
    {
      try
      {
        //var task = _tasksSubject.Value.SingleOrDefault(t => t.TaskId.Equals(id));
        //return task;
        TaskDto task = null;
        if (_tasksSubject != null)
        {
          task = _tasksSubject.Value.SingleOrDefault(t => t.Id.Equals(id));
          return task;
        }
        else
        {
          _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());

          var queryResult = await _httpClient.GetFromJsonAsync<GetTaskDetailByIdQueryResult>("api/tasks/{id}");
          var tasks = new List<TaskDto>(_tasksSubject.Value);
          tasks.Add(queryResult.Payload);
          _tasksSubject.OnNext(tasks);
          return task;
        }

      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "GetTaskById",
          ErrorMessage = ex.Message
        });
        return null;
      }
    }
    public async Task GetAllTasks()
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var result = await _httpClient.GetFromJsonAsync<GetAllTasksQueryResult>("api/tasks");

        _tasksSubject.OnNext(result.Payload);

        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = true,
          Operation = "GetAllTasks"
        });

      }
      catch (Exception ex)
      {

        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "GetAllTasks",
          ErrorMessage = ex.Message
        });
      }

    }
    public async Task CreateMyTask(CreateMyTaskCommand command)
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var response = await _httpClient.PostAsJsonAsync<CreateMyTaskCommand>("api/Tasks/MyTask", command);
        if (response.IsSuccessStatusCode)
        {
          var commandResult = await response.Content.ReadFromJsonAsync<CreateMyTaskCommandResult>();

          var tasks = new List<TaskDto>(_tasksSubject.Value);
          tasks.Add(commandResult.Payload);

          _tasksSubject.OnNext(tasks);

          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = true,
            Operation = "CreateMyTask"
          });

        }
      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "CreateMyTask",
          ErrorMessage = ex.Message
        });
      }
    }
    public async Task AssignUserWithTask(Guid id, AttachUserWithTaskCommand command)
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var task = await GetById(id);

        var response = await _httpClient.PutAsJsonAsync<AttachUserWithTaskCommand>($"api/Tasks/{id}/AssignUser", command);
        if (response.IsSuccessStatusCode)
        {
          var commandResult = await response.Content.ReadFromJsonAsync<AttachUserWithTaskCommandResult>();

          var tasks = new List<TaskDto>(_tasksSubject.Value);

          //For replacing the object
          var idx = tasks.IndexOf(task);
          if (idx >= 0)
          {
            tasks[idx] = commandResult.Payload;
          }

          _tasksSubject.OnNext(tasks);
          _apiCallResultSubject.OnNext(new ApiCallResult<string>()
          {
            IsSucceed = true,
            Operation = "AttachUserWithTask"
          });

        }
      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "AttachUserWithTask",
          ErrorMessage = ex.Message
        });
      }
    }
    public async Task GetMyAllTasks()
    {
      try
      {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _authHandler.GetAuthAccessToken());
        var result = await _httpClient.GetFromJsonAsync<GetMyAllTasksQueryResult>("api/tasks/MyTask");

        _tasksSubject.OnNext(result.Payload);

        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = true,
          Operation = "GetMyAllTasks"
        });

      }
      catch (Exception ex)
      {
        _apiCallResultSubject.OnNext(new ApiCallResult<string>()
        {
          IsSucceed = false,
          Operation = "GetMyAllTasks",
          ErrorMessage = ex.Message
        });
      }

    }
    //private async Task<TaskDto> GetTaskById(Guid taskId)
    //{
    //  var task = _tasksSubject.Value.SingleOrDefault(t => t.TaskId.Equals(taskId));
    //  return task;


    //  //TaskDto task = null;
    //  //if (_tasksSubject != null)
    //  //{
    //  //  task = _tasksSubject.Value.SingleOrDefault(t => t.TaskId.Equals(taskId));
    //  //}
    //  //else
    //  //{
    //  //  await GetToken();

    //  //  var response = await _http.GetAsync($"https://localhost:5001/api/tasks/{taskId}");
    //  //  if (response.IsSuccessStatusCode)
    //  //  {
    //  //    string content = await response.Content.ReadAsStringAsync();
    //  //    task = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModelForSinglePayload>(content).Payload;
    //  //  }
    //  //}

    //  //return task;
    //}

  }
}
