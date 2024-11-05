using Application.TodosFeatures.Commands;
using Application.TodosFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoWebApi.Models.RequestModels;

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

        [HttpPost("change-state")]
        public async Task<IActionResult> ChagneStateRequest([FromBody] ChangeTodoStateRequest request)
        {

            return await Mediator.Send(new ChangeStateTodoCommand
            {
                Id = request.Id,
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTodoRequest([FromBody] CreateTodoRequest request)
        {

            return await Mediator.Send(new AddTodoCommand
            {
                Date = request.Date,
                TodoBody = request.TodoBody,
            });
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditTodoRequest([FromBody] EditTodoRequest request)
        {

            return await Mediator.Send(new EditTodoCommand
            {
                Date = request.Date,
                TodoBody = request.TodoBody,
            });
        }
    }
}
