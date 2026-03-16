namespace LabScore.io.Server.Model
{
    public class Alternativa
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Numero { get; set; } 
        public string Texto { get; set; } = string.Empty;
        public Guid QuestaoId { get; set; }
    }
}