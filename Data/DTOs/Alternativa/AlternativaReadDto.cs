using LabScore.io.Server.Data.DTOs.Questao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabScore.io.Server.Data.DTOs.Alternativa
{
    public class AlternativaReadDto
    {
        public Guid Id { get; set; }
        public string Texto { get; set; } = string.Empty;
        public required QuestaoDto Questao { get; set; }
        public bool EhCorreta { get; set; }
    }

    public class QuestaoDto
    {
        public Guid Id { get; set; }
        public string Enunciado { get; set; } = string.Empty;
        public string Disciplina { get; set; } = string.Empty;
    }

}