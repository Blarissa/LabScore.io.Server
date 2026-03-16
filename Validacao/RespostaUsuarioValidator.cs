using FluentValidation;
using LabScore.io.Server.Model;

namespace LabScore.io.Server.Validacao
{
    public class RespostaUsuarioValidator : AbstractValidator<RespostaUsuario>
    {
        public RespostaUsuarioValidator()
        {
            RuleFor(r => r.QuestaoId).NotEqual(Guid.Empty);
            RuleFor(r => r.AlternativaEscolhidaId)
                .NotEmpty();
        }
    }
}
