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

    public TasksService(HttpClient http, AuthHandler auth)
    {
      _auth = auth;
      _http = http;
    }

    public List<TaskDetail> taskDetails = null;
    public ResponseModel responseModel = null;
    

    public async Task<HttpResponseMessage> Create(TaskDetail taskDetail)
    {
      //_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetAuthAccessToken());
      await GetToken();
      var response = await _http.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"https://localhost:5001/api/tasks")
      {
        Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(taskDetail), System.Text.Encoding.UTF8, "application/json")
      });
      return response;
    }

    public async Task<HttpResponseMessage> Update(TaskDetail taskDetail, Guid taskId)
    {
      //_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetAuthAccessToken());
      await GetToken();
      var response = await _http.SendAsync(new HttpRequestMessage(HttpMethod.Put, $"https://localhost:5001/api/tasks/{taskId}")
      {
        Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(taskDetail), System.Text.Encoding.UTF8, "application/json")
      });
      return response;
    }

    public async Task<HttpResponseMessage> Delete(Guid? id)
    {
      //_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetAuthAccessToken());
      await GetToken();

      var response = await _http.DeleteAsync($"https://localhost:5001/api/tasks/{id}");
      return response;
    }

    public async Task<ResponseModel> GetTasks()
    {
      //_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetAuthAccessToken());
      await GetToken();

      var response = await _http.GetAsync("https://localhost:5001/api/tasks");
      if (response.IsSuccessStatusCode)
      {
        string content = await response.Content.ReadAsStringAsync();
        responseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(content);
        taskDetails = responseModel.Payload;
        //State.OnInitSetTodos(responseModel.Payload);
        return responseModel;
      }
      return null;
    }

    public async Task<TaskDetail> GetTaskById(Guid taskId)
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
      return null;
    }
    private async Task<AuthenticationHeaderValue> GetToken()
    {
      return _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetAuthAccessToken());
    }
  }
}
