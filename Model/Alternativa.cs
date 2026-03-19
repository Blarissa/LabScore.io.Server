namespace LabScore.io.Server.Model
{
    public class Alternativa
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid QuestaoId { get; set; }
        public string Texto { get; set; } = string.Empty;
        public bool EhCorreta { get; set; }
        public virtual Questao Questao { get; set; }
    }
}