using System.ComponentModel.DataAnnotations;

namespace LabScore.io.Server.Data.DTOs.Simulado
{
    public class SimuladoConjuntoCreateDto
    {
        [Required(ErrorMessage = "Informe ao menos uma questão.")]
        [MinLength(1, ErrorMessage = "Informe ao menos uma questão.")]
        public List<Guid> QuestoesIds { get; set; } = new();
    }
}