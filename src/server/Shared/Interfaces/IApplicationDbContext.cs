using Microsoft.EntityFrameworkCore;
using Domain;

namespace Shared;

public interface IApplicationDbContext
{
    DbSet<Todo> Todos { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();
}
