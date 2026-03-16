namespace LabScore.io.Server.Exceptions
{
    public class LabScoreException : Exception
    {
        public LabScoreException(string mensagem) : base(mensagem) { }
    }

    public class EntidadeNaoEncontradaException : LabScoreException
    {
        public EntidadeNaoEncontradaException(string entidade, Guid id)
            : base($"{entidade} com ID {id} não foi encontrada.") { }
    }

    public class SimuladoInvalidoException : LabScoreException
    {
        public SimuladoInvalidoException(string mensagem) : base(mensagem) { }
        public SimuladoInvalidoException() 
            : base("O simulado enviado contém dados inválidos ou inconsistentes.") { }
    }

    public class AlternativaInconsistenteException : LabScoreException
    {
        public AlternativaInconsistenteException(int qId, int aId)
            : base($"A alternativa {aId} não pertence à questão {qId}.") { }
    }
}
