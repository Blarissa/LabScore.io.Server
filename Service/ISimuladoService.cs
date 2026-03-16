using LabScore.io.Server.Model;

namespace LabScore.io.Server.Service
{
    public interface ISimuladoService
    {
        Task<Simulado> ProcessarSimuladoAsync(Simulado simulado);
        Task<Simulado?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Simulado>> ListarTodosAsync();
    }
}