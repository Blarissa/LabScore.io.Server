namespace LabScore.io.Server.Model
{
    public class RespostaUsuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SimuladoId { get; set; }
        public Guid QuestaoId { get; set; }
        public int AlternativaEscolhidaId { get; set; }
        public bool EhCorreta { get; set; }
    }
}