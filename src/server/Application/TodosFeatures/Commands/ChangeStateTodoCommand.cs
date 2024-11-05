﻿using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;


namespace Application.TodosFeatures.Commands;

public class ChangeStateTodoCommand : IRequest<IActionResult>
{
    public Guid Id { get; set; }
    public string TodoBody { get; set; }
    public DateTime Date { get; set; }
}

public class GChangeStateTodoCommandHandler : IRequestHandler<ChangeStateTodoCommand, IActionResult>
{
    private readonly IApplicationDbContext _context;

    public GChangeStateTodoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(ChangeStateTodoCommand request, CancellationToken cancellationToken)
    {
        var todoRequested = await _context.Todos.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        todoRequested.TodoBody =  request.TodoBody;
        todoRequested.ExpirenceDate = request.Date;

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return new OkObjectResult(todoRequested);
    }
}
