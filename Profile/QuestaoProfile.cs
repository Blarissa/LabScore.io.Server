using AutoMapper;
using LabScore.io.Server.Data.DTOs.Alternativa;
using LabScore.io.Server.Data.DTOs.Questao;
using LabScore.io.Server.Model;

namespace LabScore.io.Server.Profile
{
    public class QuestaoProfile : AutoMapper.Profile
    {
        public QuestaoProfile()
        {
            CreateMap<QuestaoCreateDto, Questao>()
                .ForMember(dest => dest.Alternativas,
                    opt => opt.MapFrom(src => src.Alternativas));

            CreateMap<AlternativaCreateDto, Alternativa>()
                .ForMember(dest => dest.EhCorreta, opt => opt.Ignore());

            CreateMap<Questao, QuestaoReadDto>();

            CreateMap<Alternativa, Data.DTOs.Questao.AlternativaReadDto>();
        }
    }
}