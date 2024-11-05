using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;


namespace Application.TodosFeatures.Commands;

public class AddTodoCommand : IRequest<IActionResult>
{
    public string TodoBody { get; set; }
    public DateTime Date { get; set; }
}

public class AddTodoCommandHandler : IRequestHandler<AddTodoCommand, IActionResult>
{
    private readonly IApplicationDbContext _context;

    public AddTodoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(AddTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Todo
        {
            ExpirenceDate = request.Date,
            TodoBody = request.TodoBody,
        };

        _context.Todos.Add(todo);

        return new OkObjectResult(todo);
    }
}

