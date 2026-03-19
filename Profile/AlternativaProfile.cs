using LabScore.io.Server.Data.DTOs.Alternativa;
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