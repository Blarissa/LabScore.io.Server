namespace LabScore.io.Server.Model
{
    public class Questao
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Enunciado { get; set; } = string.Empty;
        public List<Alternativa> Alternativas { get; set; } = new();
        public int AlternativaCorretaId { get; set; }
        public string Disciplina { get; set; } = string.Empty;
    }
}
