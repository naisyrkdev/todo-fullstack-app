using Domain;

namespace Application.Models;

public class TodoClientModel
{
    public Guid Id { get; set; }
    public string TodoBody { get; set; }
    public bool IsDone { get; set; }
    public DateTime ExpirenceDate { get; set; }
    public string ExpirationDateString { get; set; }
}

public static class TodoClientModelFactory
{
    public static TodoClientModel Create(Todo dbModel)
    {
        return new TodoClientModel()
        {
            Id = dbModel.Id,
            ExpirenceDate = dbModel.ExpirenceDate,
            IsDone = dbModel.IsDone,    
            TodoBody = dbModel.TodoBody,
            ExpirationDateString = dbModel.ExpirenceDate.ToString("yyyy/MM/dd"),
        };
    }

    public static List<TodoClientModel> CreateList(List<Todo> dbModel)
    {
        return dbModel.Select(Create).ToList();
    }
}
