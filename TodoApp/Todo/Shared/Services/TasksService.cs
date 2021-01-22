using Fistix.Training.Domain.Commands.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Todo.Shared.Models;

namespace Todo.Shared.Services
{
  public class TasksService
  {
    private readonly HttpClient _http = null;
    private readonly AuthHandler _auth = null;
    private List<TaskDetail> _source;
    public TasksService(HttpClient http, AuthHandler auth, List<TaskDetail> source)
    {
      _auth = auth;
      _http = http;
      _source = source;
    }

    public List<TaskDetail> taskDetails = null;
    public ResponseModel responseModel = null;
    

    public async Task<HttpResponseMessage> Create(CreateTaskCommand command)
    {
      //_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetAuthAccessToken());
      await GetToken();
      var response = await _http.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"https://localhost:5001/api/tasks")
      {
        Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(command), System.Text.Encoding.UTF8, "application/json")
      });

      //To add Created task into Grid
      if (response.IsSuccessStatusCode)
      {
        string content = await response.Content.ReadAsStringAsync();
        var createdTask = Newtonsoft.Json.JsonConvert.DeserializeObject<TaskDetail>(content);
        //taskDetails.Add(createdTask);
        _source.Add(createdTask);
      }
      return response;
    }

    public async Task<HttpResponseMessage> Update(UpdateTaskCommand command, Guid taskId)
    {
      //_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetAuthAccessToken());
      await GetToken();
      var response = await _http.SendAsync(new HttpRequestMessage(HttpMethod.Put, $"https://localhost:5001/api/tasks/{taskId}")
      {
        Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(command), System.Text.Encoding.UTF8, "application/json")
      });

      //To add Updated task into Grid
      if (response.IsSuccessStatusCode)
      {
        //Remove un-updated task
        var task = await GetTaskById(taskId);
        //taskDetails.Remove(task);
        _source.Remove(task);

        //Add updated task
        string content = await response.Content.ReadAsStringAsync();
        var updatedTask = Newtonsoft.Json.JsonConvert.DeserializeObject<TaskDetail>(content);
        //taskDetails.Add(updatedTask);
        _source.Add(updatedTask);
      }


      return response;
    }

    public async Task<HttpResponseMessage> Delete(Guid? id)
    {
      var task = await GetTaskById((Guid)id);

      //_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetAuthAccessToken());
      await GetToken();

      var response = await _http.DeleteAsync($"https://localhost:5001/api/tasks/{id}");
      if (response.IsSuccessStatusCode)
      {
        //taskDetails.Remove(task);
        _source.Remove(task);
      }

      return response;
    }

    public async Task<ResponseModel> GetTasks()
    {
      if (_source != null && _source.Count > 0)//(taskDetails != null)
      {
        return responseModel;
      }
      else
      {
        //_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetAuthAccessToken());
        await GetToken();

        var response = await _http.GetAsync("https://localhost:5001/api/tasks");
        if (response.IsSuccessStatusCode)
        {
          string content = await response.Content.ReadAsStringAsync();
          responseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(content);
          //taskDetails = responseModel.Payload;
          _source = responseModel.Payload;
          //State.OnInitSetTodos(responseModel.Payload);
          return responseModel;
        }
      }
      return null;
    }

    public async Task<TaskDetail> GetTaskById(Guid taskId)
    {
      if (taskDetails!= null)
      {
        //var task = taskDetails.FirstOrDefault(t => t.TaskId.Equals(taskId));
        var task = _source.FirstOrDefault(t => t.TaskId.Equals(taskId));
        //taskDetails.Find(t => t.TaskId.Equals(taskId));
        return task;
      }
      else
      {
        //_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetAuthAccessToken());
        await GetToken();
        var response = await _http.GetAsync($"https://localhost:5001/api/tasks/{taskId}");
        if (response.IsSuccessStatusCode)
        {
          string content = await response.Content.ReadAsStringAsync();
          var taskDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModelForSinglePayload>(content).Payload;
          return taskDetails;
        }
      }
      return null;
    }

    private async Task<AuthenticationHeaderValue> GetToken()
    {
      return _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetAuthAccessToken());
    }
  }
}
