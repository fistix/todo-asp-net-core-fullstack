using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Todo.Shared.Models;

namespace Todo.Shared.Services
{
  public class RequestHandler
  {
    private readonly HttpClient _client = null;
    private readonly AuthHandler _auth = null;
    public RequestHandler(HttpClient client, AuthHandler auth)
    {
      _auth = auth;
      _client = client;
    }
    public async Task<HttpResponseMessage> SendTodoRequest(TaskDetail task, HttpMethod method, string url)
    {
      _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetAuthAccessToken());
      if (task != null)
      {
        return await _client.SendAsync(new HttpRequestMessage(method, url)
        {
          Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(task), System.Text.Encoding.UTF8, "application/json")
        });
      }
      else
      {
        return await _client.GetAsync(url);
      }

    }
  }
}
