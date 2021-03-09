using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fistix.Training.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PayPalController : ControllerBase
  {
    private readonly IMediator _mediator = null;
    private readonly HttpClient _httpClient = null;


    public PayPalController(IMediator mediator, HttpClient httpClient)
    {
      _mediator = mediator;
      _httpClient = httpClient;
    }



  }
}
