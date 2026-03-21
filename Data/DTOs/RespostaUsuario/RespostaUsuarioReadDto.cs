namespace LabScore.io.Server.Data.DTOs.RespostaUsuario
{
    public class RespostaUsuarioReadDto
    {
        public Guid Id { get; set; }
        public required SimuladoDto SimuladoId { get; set; }
        public required QuestaoDto QuestaoId { get; set; }
        public required AlternativaDto AlternativaEscolhida { get; set; }
        public required AlternativaDto AlternativaCorreta { get; set; }
    }

    public class AlternativaDto
    {
        public Guid Id { get; set; }
        public string Texto { get; set; } = string.Empty;

        public bool EhCorreta { get; set; }
    }

    public class QuestaoDto
    {
        public Guid Id { get; set; }
        public string Enunciado { get; set; } = string.Empty;
        public string Disciplina { get; set; } = string.Empty;
    }

    public class SimuladoDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime DataRealizacao { get; set; }
        public double PontuacaoFinal { get; set; }

    }

}
