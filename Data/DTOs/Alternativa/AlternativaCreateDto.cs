namespace LabScore.io.Server.Data.DTOs.Alternativa
{
    public class AlternativaCreateDto
    {
        public Guid QuestaoId { get; set; }
        public string Texto { get; set; } = string.Empty;
        public bool EhCorreta { get; set; }
    }
}