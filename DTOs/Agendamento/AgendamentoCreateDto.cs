using System;

namespace MinhaApi.DTOs.Agendamento
{
    public class AgendamentoCreateDto
    {
        public Guid UsuarioId { get; set; }
        public int ServicoId { get; set; }
        public int ProfissionalId { get; set; }
        public DateTime Data { get; set; }
    }
}
