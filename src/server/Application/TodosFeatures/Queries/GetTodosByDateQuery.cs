using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.TodosFeatures.Queries;

public class GetTodosByDateQuery : IRequest<IActionResult>
{
    public DateTime FilterDate { get; set; }
}

public class GetTodosByDateQueryHandler : IRequestHandler<GetTodosByDateQuery, IActionResult>
{
    private readonly IApplicationDbContext _context;

    public GetTodosByDateQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(GetTodosByDateQuery request, CancellationToken cancellationToken)
    {
        var todosFound = await _context.Todos
            .Where(x => x.ExpirenceDate.Year == request.FilterDate.Year)
            .Where(x => x.ExpirenceDate.Month == request.FilterDate.Month)
            .Where(x => x.ExpirenceDate.Day == (request.FilterDate.Day + 1))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
            
        if (todosFound == null)
            return new BadRequestObjectResult("An error has occured");

        var todoClientModel = TodoClientModelFactory.CreateList(todosFound);

        return new OkObjectResult(todoClientModel);
    }
}

