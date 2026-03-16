namespace LabScore.io.Server.Data.DTOs.Questao
{
    public class QuestaoCreateDto
    {
        public string Enunciado { get; set; }
        public string Disciplina { get; set; }
        public List<AlternativaCreateDto> Alternativas { get; set; }
        public int AlternativaCorretaId { get; set; }
    }

    public class AlternativaCreateDto
    {
        public string Texto { get; set; }
    }
}
