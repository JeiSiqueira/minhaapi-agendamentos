using System;

namespace MinhaApi.Models
{
    public class Agendamento
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int ServicoId { get; set; }
        public Servico? Servico { get; set; }

        public int ProfissionalId { get; set; }
        public Profissional? Profissional { get; set; }
    }
}
