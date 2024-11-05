using Application.TodosFeatures.Commands;
using Application.TodosFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TodoWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodosController : ApiControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetTodosRequest()
        {
            return await Mediator.Send(new GetTodosQuery
            {
               
            });
        }

        //[Authorize]
        //[HttpPost("equip")]
        //public async Task<IActionResult> ChangeStateTodoRequest([FromBody] BasicRequest<Guid> request)
        //{

        //    return await Mediator.Send(new ChangeStateTodoCommand
        //    {
                
        //    });
        //}
    }
}
