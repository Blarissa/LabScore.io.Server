namespace LabScore.io.Server.Data.DTOs.Questao
{
    public class QuestaoReadDto
    {
        public Guid Id { get; set; }
        public required string Enunciado { get; set; }
        public required string Disciplina { get; set; }
        public required List<AlternativaReadDto> Alternativas { get; set; }
    }

    public class AlternativaReadDto
    {
        public Guid Id { get; set; }
        public required string Texto { get; set; }
    }
}
