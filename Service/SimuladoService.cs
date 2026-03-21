using LabScore.io.Server.Exceptions;
using LabScore.io.Server.Model;
using LabScore.io.Server.Repository;

namespace LabScore.io.Server.Service
{
    public class SimuladoService : ISimuladoService
    {
        private readonly IRepository<Simulado> _simuladoRepository;
        private readonly IRepositoryQuestao _questaoRepository;

        public SimuladoService(
            IRepository<Simulado> simuladoRepository,
            IRepositoryQuestao questaoRepository
            )
        {
            _simuladoRepository = simuladoRepository;
            _questaoRepository = questaoRepository;
        }

        public async Task<Simulado> ProcessarSimuladoAsync(Simulado simulado)
        {
            if (simulado.RespostasEnviadas == null || !simulado.RespostasEnviadas.Any())
                throw new SimuladoInvalidoException("Não é possível processar um simulado sem respostas.");

            double acertos = 0;

            foreach (var resposta in simulado.RespostasEnviadas)
            {
                var questaoOriginal = await _questaoRepository.ObterPorIdAsync(resposta.QuestaoId);
                resposta.Questao = questaoOriginal;

                var alternativaEscolhida = questaoOriginal.Alternativas
                    .FirstOrDefault(a => a.Id == resposta.AlternativaEscolhidaId);

                if (alternativaEscolhida == null)
                    throw new EntidadeNaoEncontradaException("Alternativa escolhida", resposta.AlternativaEscolhidaId);

                resposta.AlternativaEscolhida = alternativaEscolhida;
                resposta.EhCorreta = alternativaEscolhida.EhCorreta;

                if (resposta.EhCorreta)
                    acertos++;
            }

            simulado.PontuacaoFinal = acertos;
            simulado.DataRealizacao = DateTime.UtcNow;

            await _simuladoRepository.AdicionarAsync(simulado);
            await _simuladoRepository.SalvarAlteracoesAsync();

            return simulado;
        }

        public async Task<Simulado?> ObterPorIdAsync(Guid id)
        {
            var simulado = await _simuladoRepository.ObterPorIdAsync(id);

            if (simulado == null)
                throw new EntidadeNaoEncontradaException("Simulado", id);

            return simulado;
        }

        public async Task<IEnumerable<Simulado>> ListarTodosAsync()
        {
            return await _simuladoRepository.ListarTodasAsync();
        }
    }
   }
