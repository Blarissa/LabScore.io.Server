using LabScore.io.Server.Model;

namespace LabScore.io.Server.Repository
{
    public interface IRepository<T>
    {
        Task AdicionarAsync(T t);
        Task<T> ObterPorIdAsync(Guid id);
        Task<IEnumerable<T>> ListarTodasAsync();
        Task<bool> SalvarAlteracoesAsync();
    }
}
