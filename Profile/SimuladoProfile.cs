using AutoMapper;
using LabScore.io.Server.Data.DTOs.Simulado;
using LabScore.io.Server.Model;

namespace LabScore.io.Server.Profile
{
    public class SimuladoProfile : AutoMapper.Profile
    {
        public SimuladoProfile()
        {
            CreateMap<ResponderSimuladoCreateDto, Simulado>();
            CreateMap<Simulado, ResponderSimuladoReadDto>();

            CreateMap<SimuladoConjuntoCreateDto, Simulado>();
            CreateMap<Simulado, SimuladoConjuntoReadDto>()
                .ForMember(dest => dest.TotalQuestoes,
                    opt => opt.MapFrom(src => src.RespostasEnviadas.Count));
        }
    }
}
