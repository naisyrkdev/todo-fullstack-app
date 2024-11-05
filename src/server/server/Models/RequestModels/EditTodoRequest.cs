namespace TodoWebApi.Models.RequestModels;

public class EditTodoRequest : CreateTodoRequest
{
    public Guid Id { get; set; }
}
