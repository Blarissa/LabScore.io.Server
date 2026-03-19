using LabScore.io.Server.Data;
using LabScore.io.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace LabScore.io.Server.Repository
{
    public class AlternativaRepository : IRepository<Alternativa>
    {

        private readonly AppDbContext _context;

        public AlternativaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Alternativa alternativa)
        {
            await _context.Alternativas.AddAsync(alternativa);
        }

        public async Task<IEnumerable<Alternativa>> ListarTodasAsync()
        {
            return await _context.Alternativas
                .ToListAsync();
        }

        public async Task<Alternativa> ObterPorIdAsync(Guid id)
        {
            return await _context.Alternativas
                .FirstOrDefaultAsync(s => s.Id == id) ??
                throw new KeyNotFoundException($"Alternativa com ID {id} não encontrado");

        }

       
        public async Task<bool> SalvarAlteracoesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
