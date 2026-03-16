using LabScore.io.Server.Model;

namespace LabScore.io.Server.Service
{
    public interface IQuestaoService
    {
        Task<IEnumerable<Questao>> RecuperarTodasAsync();
        Task<Questao?> RecuperarPorIdAsync(Guid id);
        Task<Questao> CadastrarAsync(Questao questao);
        Task<IEnumerable<Questao>> CadastrarEmLoteAsync(IEnumerable<Questao> questoes);
    }
}
