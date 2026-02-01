namespace MinhaApi.DTOs.Agendamento;

public class AgendamentoResponseDto
{
    public Guid Id { get; set; }
    public DateTime Data { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Servico { get; set; } = string.Empty;
    public string Profissional { get; set; } = string.Empty;
}
