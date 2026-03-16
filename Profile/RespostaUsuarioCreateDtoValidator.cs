using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LabScore.io.Server.Data.DTOs.RespostaUsuario;

namespace LabScore.io.Server.Profile
{
    public class RespostaUsuarioCreateDtoValidator : AbstractValidator<RespostaUsuarioCreateDto>
    {
        public RespostaUsuarioCreateDtoValidator()
        {
            RuleFor(x => x.QuestaoId)
                .NotEmpty().WithMessage("O ID da questão é obrigatório.");

            RuleFor(x => x.AlternativaEscolhidaId)
                .GreaterThanOrEqualTo(0).WithMessage("O ID da alternativa deve ser válido.");
        }
    }
}