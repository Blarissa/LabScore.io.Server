namespace LabScore.io.Server.Data.DTOs.RespostaUsuario
{
    public class RespostaUsuarioCreateDto
    {
        public Guid QuestaoId { get; set; }
        public Guid AlternativaEscolhidaId { get; set; }
        public Guid  SimuladoId { get; set; }
    }
}