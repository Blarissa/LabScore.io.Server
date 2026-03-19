namespace LabScore.io.Server.Model
{
    public class Questao
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Enunciado { get; set; } = string.Empty;
        public virtual List<Alternativa> Alternativas { get; set; } = new();
        public string Disciplina { get; set; } = string.Empty;
    }
}
