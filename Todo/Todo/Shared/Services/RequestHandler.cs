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
    public async Task<HttpResponseMessage> SendTodoRequest(TodoDetail todo, HttpMethod method)
    {
      _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetAuthAccessToken());
      return await _client.SendAsync(new HttpRequestMessage(method, "todo") 
      { 
        Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(todo), System.Text.Encoding.UTF8, "application/json")
      });

    }
  }
}
