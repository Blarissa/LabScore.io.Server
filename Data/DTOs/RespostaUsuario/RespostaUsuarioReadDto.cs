namespace LabScore.io.Server.Data.DTOs.RespostaUsuario
{
    public class RespostaUsuarioReadDto
    {
        public Guid Id { get; set; }
        public Guid SimuladoId { get; set; }
        public Guid QuestaoId { get; set; }
        public int AlternativaEscolhidaId { get; set; }

        public bool EhCorreta { get; set; }
        public AlternativaCorretaReadDto? AlternativaCorreta { get; set; }
    }

    public class AlternativaCorretaReadDto
    {
        public Guid Id { get; set; }
        public string Texto { get; set; } = string.Empty;
    }
}
