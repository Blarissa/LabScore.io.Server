namespace LabScore.io.Server.Model
{
    public class RespostaUsuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SimuladoId { get; set; }
        public Guid QuestaoId { get; set; }
        public Guid AlternativaEscolhidaId { get; set; }
        public bool EhCorreta { get; set; }
        public required virtual Alternativa AlternativaEscolhida { get; set; }
        public required virtual Simulado Simulado { get; set; }
        public required virtual Questao Questao { get; set; }

    }
}