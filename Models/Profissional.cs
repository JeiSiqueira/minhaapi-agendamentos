namespace MinhaApi.Models;

public class Profissional
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    // Soft delete flag
    public bool IsDeleted { get; set; }
}
