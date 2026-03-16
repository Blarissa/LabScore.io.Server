using AutoMapper;
using LabScore.io.Server.Data.DTOs.RespostaUsuario;
using LabScore.io.Server.Data.DTOs.Simulado;
using LabScore.io.Server.Model;

namespace LabScore.io.Server.Profile
{
    public class SimuladoProfile : AutoMapper.Profile
    {
        public SimuladoProfile()
        {
            CreateMap<SimuladoCreateDto, Simulado>();
            CreateMap<Simulado, SimuladoResultDto>();
        }
    }
}
