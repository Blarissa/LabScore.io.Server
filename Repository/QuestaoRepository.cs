using LabScore.io.Server.Data;
using LabScore.io.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace LabScore.io.Server.Repository
{
    public class QuestaoRepository : IRepositoryQuestao
    {
        private readonly AppDbContext _context;

        public QuestaoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Questao> ObterPorIdAsync(Guid id)
        {
            return await _context.Questoes
                .Include(q => q.Alternativas)
                .FirstOrDefaultAsync(q => q.Id == id) ?? 
                throw new KeyNotFoundException($"Questão com ID {id} não encontrada.");
        }

        public async Task<IEnumerable<Questao>> ListarTodasAsync()
        {
            return await _context.Questoes.Include(q => q.Alternativas).ToListAsync();
        }

        public async Task AdicionarAsync(Questao questao)
        {
            await _context.Questoes.AddAsync(questao);
        }

        public async Task<bool> SalvarAlteracoesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<IEnumerable<Questao>> CadastrarEmLoteAsync(IEnumerable<Questao> questoes)
        {
            if (questoes is null)
                throw new ArgumentNullException(nameof(questoes));

            var lista = questoes.ToList();
            if (lista.Count == 0)
                return lista;

            await _context.Questoes.AddRangeAsync(lista);

            return lista;
        }

        public async Task<Alternativa> ObterAlternativaCorretaAsync(Guid questaoId)
        {
            return await _context.Alternativas
                .FirstOrDefaultAsync(a => a.QuestaoId == questaoId && a.EhCorreta)
                ?? throw new KeyNotFoundException(
                    $"Questão {questaoId} não possui alternativa correta configurada.");
        }
    }
}
