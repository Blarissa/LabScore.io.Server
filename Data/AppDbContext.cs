using LabScore.io.Server.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LabScore.io.Server.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Questao> Questoes { get; set; }
        public DbSet<Alternativa> Alternativas { get; set; }
        public DbSet<Simulado> Simulados { get; set; }
        public DbSet<RespostaUsuario> RespostasUsuarios { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Questao>().HasKey(q => q.Id);
            modelBuilder.Entity<Alternativa>().HasKey(a => a.Id);
            modelBuilder.Entity<Simulado>().HasKey(s => s.Id);
            modelBuilder.Entity<RespostaUsuario>().HasKey(ru => ru.Id);

            modelBuilder.Entity<Alternativa>()
                .HasOne(a => a.Questao)
                .WithMany(q => q.Alternativas)
                .HasForeignKey(a => a.QuestaoId);

            modelBuilder.Entity<RespostaUsuario>()
                .HasOne(ru => ru.Simulado)
                .WithMany(s => s.RespostasEnviadas)
                .HasForeignKey(ru => ru.SimuladoId);

            modelBuilder.Entity<RespostaUsuario>()
                .HasOne(ru => ru.Questao)
                .WithMany()
                .HasForeignKey(ru => ru.QuestaoId);

            modelBuilder.Entity<RespostaUsuario>()
                .HasOne(ru => ru.AlternativaEscolhida)
                .WithMany()
                .HasForeignKey(ru => ru.AlternativaEscolhidaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
