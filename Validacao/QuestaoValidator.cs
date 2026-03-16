using FluentValidation;
using LabScore.io.Server.Model;

namespace LabScore.io.Server.Validacao
{
    public class QuestaoValidator : AbstractValidator<Questao>
    {
        public QuestaoValidator()
        {
            RuleFor(q => q.Enunciado)
                .NotEmpty().WithMessage("O enunciado da questão não pode ser vazio.")
                .MinimumLength(10).WithMessage("O enunciado deve ter pelo menos 10 caracteres.");

            RuleFor(q => q.Disciplina)
                .NotEmpty().WithMessage("A disciplina deve ser informada.");

            RuleFor(q => q.Alternativas)
                .Must(a => a.Count >= 2).WithMessage("A questão deve ter pelo menos 2 alternativas.");

            RuleFor(q => q.AlternativaCorretaId)
                .NotEmpty().WithMessage("Você deve indicar qual é a alternativa correta.");
        }
    }
}
