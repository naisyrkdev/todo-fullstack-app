namespace Domain;

public class Todo
{
    public Guid Id { get; set; }
    public string TodoBody { get; set; }
    public bool IsDone { get; set; }
    public DateTime ExpirenceDate { get; set; }
}
