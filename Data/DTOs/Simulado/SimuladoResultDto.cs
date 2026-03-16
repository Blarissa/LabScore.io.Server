using LabScore.io.Server.Data.DTOs.RespostaUsuario;

namespace LabScore.io.Server.Data.DTOs.Simulado
{
    // O que o React recebe de volta (O Veredito)
    public class SimuladoResultDto
    {
        public Guid Id { get; set; }
        public DateTime DataRealizacao { get; set; }
        public double PontuacaoFinal { get; set; }
        public required List<RespostaUsuarioReadDto> RespostasEnviadas { get; set; }
    }
}
