using FluentValidation;
using LabScore.io.Server.Data.DTOs.Questao;

namespace LabScore.io.Server.Validacao
{
    /// <summary>
    /// Valida o payload de cadastro em lote de questões.
    /// </summary>
    public class QuestaoCreateBulkValidator : AbstractValidator<List<QuestaoCreateDto>>
    {
        /// <summary>
        /// Define as regras para o lote de questões.
        /// </summary>
        public QuestaoCreateBulkValidator()
        {
            RuleFor(questoes => questoes)
                .NotNull().WithMessage("A lista de questões não pode ser nula.")
                .NotEmpty().WithMessage("A lista de questões não pode estar vazia.");

            RuleForEach(questoes => questoes)
                .NotNull().WithMessage("Uma questão do lote está nula.");
        }
    }
}