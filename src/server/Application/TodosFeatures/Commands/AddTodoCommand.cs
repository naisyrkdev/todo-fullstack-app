using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Application.TodosFeatures.Commands;

public class AddTodoCommand : IRequest<IActionResult>
{
    public string TodoBody { get; set; } = string.Empty;
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
        if (string.IsNullOrEmpty(request.TodoBody))
            return new BadRequestObjectResult("An error has occured. Body ");

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

        return new OkObjectResult("Success");
    }
}

