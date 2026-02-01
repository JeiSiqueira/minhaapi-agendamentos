using Microsoft.EntityFrameworkCore;
using MinhaApi.Models;

namespace MinhaApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Servico> Servicos { get; set; }
    public DbSet<Profissional> Profissionais { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map entities to lowercase table names
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuarios");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").HasColumnType("binary(16)");
            entity.Property(e => e.Nome).HasColumnName("nome").HasMaxLength(255).IsRequired();
            entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(255).IsRequired();
            entity.Property(e => e.Senha).HasColumnName("senha").HasMaxLength(255).IsRequired();
        });

        modelBuilder.Entity<Servico>(entity =>
        {
            entity.ToTable("servicos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Nome).HasColumnName("nome").HasMaxLength(255).IsRequired();
            entity.Property(e => e.Preco).HasColumnName("preco").HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(e => e.DuracaoMinutos).HasColumnName("duracaominutos").IsRequired();
        });

        modelBuilder.Entity<Profissional>(entity =>
        {
            entity.ToTable("profissionais");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.Nome).HasColumnName("nome").HasMaxLength(255).IsRequired();
        });

        modelBuilder.Entity<Agendamento>(entity =>
        {
            entity.ToTable("agendamentos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").HasColumnType("binary(16)");
            entity.Property(e => e.Data).HasColumnName("data").HasColumnType("datetime(6)").IsRequired();

            entity.Property(e => e.UsuarioId).HasColumnName("usuarioid").HasColumnType("binary(16)").IsRequired();
            entity.HasOne(e => e.Usuario).WithMany().HasForeignKey(e => e.UsuarioId).HasConstraintName("fk_agendamentos_usuarios");

            entity.Property(e => e.ServicoId).HasColumnName("servicoid").IsRequired();
            entity.HasOne(e => e.Servico).WithMany().HasForeignKey(e => e.ServicoId).HasConstraintName("fk_agendamentos_servicos");

            entity.Property(e => e.ProfissionalId).HasColumnName("profissionalid").IsRequired();
            entity.HasOne(e => e.Profissional).WithMany().HasForeignKey(e => e.ProfissionalId).HasConstraintName("fk_agendamentos_profissionais");
        });

        base.OnModelCreating(modelBuilder);
    }
}
