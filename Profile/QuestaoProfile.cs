using LabScore.io.Server.Data.DTOs.Questao;
using LabScore.io.Server.Model;

namespace LabScore.io.Server.Profile
{
    public class QuestaoProfile: AutoMapper.Profile
    {
        public QuestaoProfile() {
            CreateMap<QuestaoCreateDto, Questao>()
                .ForMember(dest => dest.Alternativas, opt => opt.MapFrom(src => src.Alternativas));

            CreateMap<Questao, QuestaoReadDto>();

            CreateMap<AlternativaCreateDto, Alternativa>();
            CreateMap<Alternativa, AlternativaReadDto>();
        }
    }
}
