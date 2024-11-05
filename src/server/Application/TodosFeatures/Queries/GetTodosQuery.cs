using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.TodosFeatures.Queries;

public class GetTodosQuery : IRequest<IActionResult>
{
}

public class GetTodosQuerydHandler : IRequestHandler<GetTodosQuery, IActionResult>
{
    private readonly IApplicationDbContext _context;

    public GetTodosQuerydHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var todos = await _context.Todos.ToListAsync(cancellationToken);
           
        return new OkObjectResult(todos);
    }
}

