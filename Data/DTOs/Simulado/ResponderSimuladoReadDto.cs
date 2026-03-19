using LabScore.io.Server.Data.DTOs.RespostaUsuario;

namespace LabScore.io.Server.Data.DTOs.Simulado
{
    public class ResponderSimuladoReadDto
    {
        public Guid Id { get; set; }
        public DateTime DataRealizacao { get; set; }
        public double PontuacaoFinal { get; set; }
        public required List<RespostaUsuarioReadDto> RespostasEnviadas { get; set; }
    }
}
