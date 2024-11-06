using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;


namespace Application.TodosFeatures.Commands;

public class EditTodoCommand : IRequest<IActionResult>
{
    public Guid Id { get; set; }
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
        var todoRequested = await _context.Todos.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        if (todoRequested == null)
            return new BadRequestObjectResult("");

        if (!string.IsNullOrEmpty(request.TodoBody))
            todoRequested.TodoBody = request.TodoBody;

        todoRequested.ExpirenceDate = new DateTime(request.Date.Year, request.Date.Month, (request.Date.Day + 1));
        
        _context.Todos.Update(todoRequested);

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return new OkObjectResult("success");
    }
}

