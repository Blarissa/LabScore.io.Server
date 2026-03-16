using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabScore.io.Server.Data.DTOs.Alternativa
{
    public class AlternativaCreateDto
    {
        public int Numero { get; set; }
        public string Texto { get; set; } = string.Empty;
    }
}