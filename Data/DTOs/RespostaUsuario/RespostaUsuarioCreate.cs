using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabScore.io.Server.Data.DTOs.RespostaUsuario
{
    public class RespostaUsuarioCreateDto
    {
        public Guid QuestaoId { get; set; }
        public int AlternativaEscolhidaId { get; set; }
    }
}