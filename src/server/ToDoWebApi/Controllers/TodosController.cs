using Application.TodosFeatures.Commands;
using Application.TodosFeatures.Queries;
using Microsoft.AspNetCore.Mvc;
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
            return await Mediator.Send(new GetTodosQuery());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoByIdRequest(Guid id)
        {
            return await Mediator.Send(new GetTodoByIdQuery
            {
                Id = id
            });
        }

        [HttpPost("by-date")]
        public async Task<IActionResult> GetTodosByDateRequest([FromBody] DateTime filterDate)
        {
            return await Mediator.Send(new GetTodosByDateQuery
            {
                FilterDate = filterDate
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

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveTodoRequest([FromBody] Guid id)
        {
            return await Mediator.Send(new RemoveTodoCommand
            {
                Id = id,
            });
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditTodoRequest([FromBody] EditTodoRequest request)
        {
            return await Mediator.Send(new EditTodoCommand
            {
                Id = request.Id,
                Date = request.Date,
                TodoBody = request.TodoBody,
            });
        }
    }
}
