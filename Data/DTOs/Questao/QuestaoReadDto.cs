namespace LabScore.io.Server.Data.DTOs.Questao
{
    public class QuestaoReadDto
    {
        public Guid Id { get; set; }
        public string Enunciado { get; set; }
        public string Disciplina { get; set; }
        public List<AlternativaReadDto> Alternativas { get; set; }
    }

    public class AlternativaReadDto
    {
        public Guid Id { get; set; }
        public string Texto { get; set; }
    }
}
