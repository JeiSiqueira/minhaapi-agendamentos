namespace MinhaApi.DTOs.Usuario;

public class UsuarioUpdateDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    // Do not include Senha in update DTO unless you implement password change logic
}
