using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.TodosFeatures.Queries;

public class GetTodoByIdQuery : IRequest<IActionResult>
{
    public Guid Id { get; set; }
}

public class GetTodoByIdQuerydHandler : IRequestHandler<GetTodoByIdQuery, IActionResult>
{
    private readonly IApplicationDbContext _context;

    public GetTodoByIdQuerydHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var todoFound = await _context.Todos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (todoFound == null)
            return new BadRequestObjectResult("An error has occured");

        var todoClientModel = TodoClientModelFactory.Create(todoFound);

        return new OkObjectResult(todoClientModel);
    }
}

