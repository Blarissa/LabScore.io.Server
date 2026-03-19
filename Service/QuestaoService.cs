using LabScore.io.Server.Exceptions;
using LabScore.io.Server.Model;
using LabScore.io.Server.Repository;

namespace LabScore.io.Server.Service
{
    public class QuestaoService : IQuestaoService
    {
        private readonly IRepositoryQuestao _repository;

        public QuestaoService(IRepositoryQuestao repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Questao>> RecuperarTodasAsync()
        {
            return await _repository.ListarTodasAsync();
        }

        public async Task<Questao?> RecuperarPorIdAsync(Guid id)
        {
            var questao = await _repository.ObterPorIdAsync(id);

            if (questao == null)
                throw new EntidadeNaoEncontradaException("Questão", id);

            return questao;
        }

        public async Task<Questao> CadastrarAsync(Questao questao)
        {
            PrepararQuestao(questao);

            await _repository.AdicionarAsync(questao);
            await _repository.SalvarAlteracoesAsync();

            return questao;
        }

        public async Task<IEnumerable<Questao>> CadastrarEmLoteAsync(IEnumerable<Questao> questoes)
        {
            var lista = questoes.ToList();

            foreach (var questao in lista)
            {
                PrepararQuestao(questao);
            }

            var cadastradas = await _repository.CadastrarEmLoteAsync(lista);
            await _repository.SalvarAlteracoesAsync();

            return cadastradas;
        }

        private static void PrepararQuestao(Questao questao)
        {
            if (questao.Id == Guid.Empty)
                questao.Id = Guid.NewGuid();

            if (questao.Alternativas is null)
                return;

            foreach (var alternativa in questao.Alternativas)
            {
                if (alternativa.Id == Guid.Empty)
                    alternativa.Id = Guid.NewGuid();

                alternativa.QuestaoId = questao.Id;
            }
        }
    }
}
