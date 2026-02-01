namespace MinhaApi.Models;

public class Servico
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int DuracaoMinutos { get; set; }

    // Soft delete flag
    public bool IsDeleted { get; set; }
}