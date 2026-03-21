using LabScore.io.Server.Data;
using LabScore.io.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace LabScore.io.Server.Repository
{
    public class SimuladoRepository : IRepository<Simulado>
    {
        private readonly AppDbContext _context;

        public SimuladoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Simulado simulado)
        {
            await _context.Simulados.AddAsync(simulado);
        }

        public async Task<Simulado> ObterPorIdAsync(Guid id)
        {
            return await _context.Simulados
                .Include(s => s.RespostasEnviadas)
                    .ThenInclude(r => r.Simulado)
                .Include(s => s.RespostasEnviadas)
                    .ThenInclude(r => r.Questao)
                        .ThenInclude(q => q.Alternativas)
                .FirstOrDefaultAsync(s => s.Id == id) ??
                throw new KeyNotFoundException($"Simulado com ID {id} não encontrado");
        }

        public async Task<bool> SalvarAlteracoesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<IEnumerable<Simulado>> ListarTodasAsync()
        {
            return await _context.Simulados
                .OrderByDescending(s => s.DataRealizacao)
                .ToListAsync();
        }
    }
}
