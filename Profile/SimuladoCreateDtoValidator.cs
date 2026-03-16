using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LabScore.io.Server.Data.DTOs.Simulado;

namespace LabScore.io.Server.Profile
{
    public class SimuladoCreateDtoValidator : AbstractValidator<SimuladoCreateDto>
    {
        public SimuladoCreateDtoValidator()
        {
            RuleFor(x => x.RespostasEnviadas)
                .NotNull().WithMessage("A lista de respostas não pode ser nula.")
                .NotEmpty().WithMessage("O simulado deve conter ao menos uma resposta.");

            RuleForEach(x => x.RespostasEnviadas)
                .SetValidator(new RespostaUsuarioCreateDtoValidator());
        }
    }
}