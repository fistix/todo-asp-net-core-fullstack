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
    public List<TaskDetail> Tasks = new List<TaskDetail>();
    public TasksService(HttpClient http, AuthHandler auth)
    {
      _auth = auth;
      _http = http;      
    }
    

    public async Task<HttpResponseMessage> Create(CreateTaskCommand command)
    {
      
      await GetToken();
      var response = await _http.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"https://localhost:5001/api/tasks")
      {
        Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(command), System.Text.Encoding.UTF8, "application/json")
      });
            
      if (response.IsSuccessStatusCode)
      {
        string content = await response.Content.ReadAsStringAsync();
        var createdTask = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModelForSinglePayload>(content);
        
        Tasks.Add(createdTask.Payload);
      }
      return response;
    }

    public async Task<HttpResponseMessage> Update(UpdateTaskCommand command, Guid taskId)
    {      
      await GetToken();
      var response = await _http.SendAsync(new HttpRequestMessage(HttpMethod.Put, $"https://localhost:5001/api/tasks/{taskId}")
      {
        Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(command), System.Text.Encoding.UTF8, "application/json")
      });
           
      if (response.IsSuccessStatusCode)
      {      
        Tasks.Remove(Tasks.SingleOrDefault(c => c.TaskId.Equals(taskId)));

        //Add updated task
        string content = await response.Content.ReadAsStringAsync();
        var updatedTask = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModelForSinglePayload>(content);
       
        Tasks.Add(updatedTask.Payload);
      }


      return response;
    }

    public async Task<HttpResponseMessage> Delete(Guid id)
    {      
      await GetToken();

      var response = await _http.DeleteAsync($"https://localhost:5001/api/tasks/{id}");
      if (response.IsSuccessStatusCode)
      {     
        Tasks.Remove(Tasks.SingleOrDefault(c => c.TaskId.Equals(id)));
      }

      return response;
    }

    public async Task<List<TaskDetail>> GetTasks()
    {      
      if (Tasks == null || Tasks.Count < 1)      
      {     
        await GetToken();

        var response = await _http.GetAsync("https://localhost:5001/api/tasks");
        if (response.IsSuccessStatusCode)
        {
          string content = await response.Content.ReadAsStringAsync();
          var responseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(content);
       
          Tasks = responseModel.Payload;
       
        }
      }

      return Tasks;
    }

    public async Task<TaskDetail> GetTaskById(Guid taskId)
    {
      TaskDetail task = null;
      if (Tasks != null)
      {        
        task = Tasks.SingleOrDefault(t => t.TaskId.Equals(taskId));               
      }
      else
      {        
        await GetToken();

        var response = await _http.GetAsync($"https://localhost:5001/api/tasks/{taskId}");
        if (response.IsSuccessStatusCode)
        {
          string content = await response.Content.ReadAsStringAsync();
          task= Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModelForSinglePayload>(content).Payload;          
        }
      }

      return task;
    }

    private async Task<AuthenticationHeaderValue> GetToken()
    {
      return _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetAuthAccessToken());
    }
  }
}
