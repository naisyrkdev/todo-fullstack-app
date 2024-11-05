namespace TodoWebApi.Models.RequestModels;

public class CreateTodoRequest
{
    public string TodoBody { get; set; }
    public DateTime Date { get; set; }
}
