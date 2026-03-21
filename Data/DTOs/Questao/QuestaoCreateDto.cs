using LabScore.io.Server.Data.DTOs.Alternativa;

namespace LabScore.io.Server.Data.DTOs.Questao
{
    public class QuestaoCreateDto
    {
        public required string Enunciado { get; set; }
        public required string Disciplina { get; set; }
        public required List<AlternativaCreateDto> Alternativas { get; set; }
        
    }
}
