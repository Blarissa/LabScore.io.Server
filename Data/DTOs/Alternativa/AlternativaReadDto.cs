using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabScore.io.Server.Data.DTOs.Alternativa
{
    public class AlternativaReadDto
    {
        public Guid Id { get; set; }
        public int Numero { get; set; }
        public string Texto { get; set; } = string.Empty;
        public Guid QuestaoId { get; set; }
    }
}