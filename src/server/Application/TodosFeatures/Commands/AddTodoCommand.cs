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
            return new OkObjectResult("Todo body cannot be empty");

        if(request.TodoBody.Length > 50)
            return new OkObjectResult("Todo body cannot extends 50 characters");

        var todo = new Todo
        {
            ExpirenceDate = new DateTime(request.Date.Year, request.Date.Month, (request.Date.Day + 1)),
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

