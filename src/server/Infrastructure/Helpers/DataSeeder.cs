using Domain;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public class DataSeeder
    {
        private IApplicationDbContext _db;

        public DataSeeder(IApplicationDbContext db)
        {
            _db = db;
        }

        public void SeedData()
        {
            _db.Todos.AddRange(GetInitialData());
            _db.SaveChanges();
        }

        private List<Todo> GetInitialData()
        {
            return new List<Todo>()
            {
                new Todo
                {
                    Id = Guid.NewGuid(),
                    ExpirenceDate = DateTime.Now,
                    IsDone = true,
                    TodoBody = "Todo 1"
                },
                new Todo
                {
                    Id = Guid.NewGuid(),
                    ExpirenceDate = DateTime.Now,
                    IsDone = false,
                    TodoBody = "Todo 2"
                },
                new Todo
                {
                    Id = Guid.NewGuid(),
                    ExpirenceDate = DateTime.Now,
                    IsDone = false,
                    TodoBody = "Todo 3"
                }
            };
        }
    }
}
