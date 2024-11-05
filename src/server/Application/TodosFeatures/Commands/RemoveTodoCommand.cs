using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;


namespace Application.TodosFeatures.Commands;

public class RemoveTodoCommand : IRequest<IActionResult>
{
    public Guid Id { get; set; }
}

public class RemoveTodoCommandHandler : IRequestHandler<RemoveTodoCommand, IActionResult>
{
    private readonly IApplicationDbContext _context;

    public RemoveTodoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(RemoveTodoCommand request, CancellationToken cancellationToken)
    {
        var todoRequested = await _context.Todos.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        if (todoRequested == null)
            return new BadRequestObjectResult("");

        _context.Todos.Remove(todoRequested);

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

