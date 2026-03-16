using LabScore.io.Server.Data.DTOs.Questao;

namespace LabScore.io.Server.Data.DTOs.Simulado
{
    public class SimuladoConjuntoReadDto
    {
        public int TotalQuestoes { get; set; }
        public List<QuestaoReadDto> Questoes { get; set; } = new();
    }
}