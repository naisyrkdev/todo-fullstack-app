using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;


namespace Application.TodosFeatures.Commands;

public class EditTodoCommand : IRequest<IActionResult>
{
    public string TodoBody { get; set; }
    public DateTime Date { get; set; }
}

public class EditTodoCommandHandler : IRequestHandler<EditTodoCommand, IActionResult>
{
    private readonly IApplicationDbContext _context;

    public EditTodoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(EditTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Todo
        {
            ExpirenceDate = request.Date,
            TodoBody = request.TodoBody,
        };

        _context.Todos.Add(todo);

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return new OkObjectResult(todo);
    }
}

