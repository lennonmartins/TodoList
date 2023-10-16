namespace MyNamespace;
public class Todo
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public bool Feito { get; set; }
    public DateTime Data { get; set; } = DateTime.Now;
}

