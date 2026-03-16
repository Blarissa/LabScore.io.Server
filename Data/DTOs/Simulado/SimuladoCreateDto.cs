using LabScore.io.Server.Data.DTOs.RespostaUsuario;

namespace LabScore.io.Server.Data.DTOs.Simulado
{
    public class SimuladoCreateDto
    {
        public List<RespostaUsuarioCreateDto> RespostasEnviadas { get; set; } = new();
    }
}
