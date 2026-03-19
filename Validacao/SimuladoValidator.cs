using FluentValidation;
using LabScore.io.Server.Model;

namespace LabScore.io.Server.Validacao
{
    public class SimuladoValidator : AbstractValidator<Simulado>
    {
        public SimuladoValidator()
        {
            RuleFor(s => s.RespostasEnviadas)
                .NotEmpty().WithMessage("Um simulado não pode ser processado sem respostas.")
                .Must(r => r.Count > 0).WithMessage("Selecione pelo menos uma resposta.");

            RuleForEach(s => s.RespostasEnviadas).SetValidator(new RespostaUsuarioValidator());

            RuleFor(x => x.RespostasEnviadas)
                .NotNull().WithMessage("A lista de respostas não pode ser nula.")
                .NotEmpty().WithMessage("O simulado deve conter ao menos uma resposta.");

            RuleForEach(x => x.RespostasEnviadas)
                .NotNull().WithMessage("Uma resposta do simulado está nula.")
                .ChildRules(resposta =>
                {
                    resposta.RuleFor(r => r.QuestaoId)
                        .NotEmpty().WithMessage("O ID da questão é obrigatório.");

                    resposta.RuleFor(r => r.AlternativaEscolhidaId)
                        .NotEmpty().WithMessage("O ID da alternativa é obrigatório.");
                });
        }
    }
}
