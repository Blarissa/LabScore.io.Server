using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabScore.io.Server.Data.DTOs.RespostaUsuario;
using LabScore.io.Server.Data.DTOs.Simulado;
using LabScore.io.Server.Model;

namespace LabScore.io.Server.Profile
{
    public class RespostaUsuarioProfile : AutoMapper.Profile
    {
        public RespostaUsuarioProfile()
        {
            CreateMap<RespostaUsuarioCreateDto, RespostaUsuario>();
            CreateMap<RespostaUsuario, RespostaUsuarioReadDto>();
        }
    }
}