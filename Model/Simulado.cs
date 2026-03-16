namespace LabScore.io.Server.Model
{
    public class Simulado
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UsuarioId { get; set; }
        public DateTime DataRealizacao { get; set; } = DateTime.Now;
        public double PontuacaoFinal { get; set; }
        public List<RespostaUsuario> RespostasEnviadas { get; set; } = new();
    }
}
