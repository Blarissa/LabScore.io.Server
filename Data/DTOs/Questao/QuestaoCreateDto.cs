namespace LabScore.io.Server.Data.DTOs.Questao
{
    public class QuestaoCreateDto
    {
        public required string Enunciado { get; set; }
        public required string Disciplina { get; set; }
        public required List<AlternativaCreateDto> Alternativas { get; set; }
        public required int AlternativaCorretaId { get; set; }
        
    }

    public class AlternativaCreateDto
    {
        public required string Texto { get; set; }
    }
}
