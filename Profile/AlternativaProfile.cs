using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabScore.io.Server.Data.DTOs.Questao;
using LabScore.io.Server.Model;

namespace LabScore.io.Server.Profile
{
    public class AlternativaProfile : AutoMapper.Profile
    {
        public AlternativaProfile()
        {
            CreateMap<AlternativaCreateDto, Alternativa>();
            CreateMap<Alternativa, AlternativaReadDto>();
        }
    }
}