using System;

namespace MinhaApi.DTOs.Agendamento
{
    public class AgendamentoUpdateDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public int ServicoId { get; set; }
        public int ProfissionalId { get; set; }
        public DateTime Data { get; set; }
    }
}
