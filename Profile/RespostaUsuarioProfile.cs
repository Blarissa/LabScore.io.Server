using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LabScore.io.Server.Data.DTOs.RespostaUsuario;
using LabScore.io.Server.Model;

namespace LabScore.io.Server.Profile
{
    public class RespostaUsuarioProfile : AutoMapper.Profile
    {
        public RespostaUsuarioProfile()
        {
            CreateMap<RespostaUsuarioCreateDto, RespostaUsuario>();

            CreateMap<RespostaUsuario, RespostaUsuarioReadDto>()
                .ForMember(dest => dest.SimuladoId, opt =>
                    opt.MapFrom(src => new SimuladoDto
                    {
                        Id = src.Simulado.Id,
                        UsuarioId = src.Simulado.UsuarioId,
                        DataRealizacao = src.Simulado.DataRealizacao,
                        PontuacaoFinal = src.Simulado.PontuacaoFinal
                    }))
                .ForMember(dest => dest.QuestaoId, opt =>
                    opt.MapFrom(src => new QuestaoDto
                    {
                        Id = src.Questao.Id,
                        Enunciado = src.Questao.Enunciado,
                        Disciplina = src.Questao.Disciplina
                    }))
                .ForMember(dest => dest.AlternativaEscolhida, opt =>
                    opt.MapFrom(src => new AlternativaDto
                    {
                        Id = src.AlternativaEscolhida.Id,
                        Texto = src.AlternativaEscolhida.Texto,
                        EhCorreta = src.AlternativaEscolhida.EhCorreta
                    }))
                .ForMember(dest => dest.AlternativaCorreta, opt =>
                    opt.MapFrom(src => ObterAlternativaCorreta(src)));
        }

        private static AlternativaDto? ObterAlternativaCorreta(RespostaUsuario src)
        {
            if (src?.Questao?.Alternativas == null || !src.Questao.Alternativas.Any())
                return null;

            var alternativaCorreta = src.Questao.Alternativas
                .FirstOrDefault(a => a.EhCorreta);

            return alternativaCorreta != null
                ? new AlternativaDto
                {
                    Id = alternativaCorreta.Id,
                    Texto = alternativaCorreta.Texto,
                    EhCorreta = alternativaCorreta.EhCorreta
                }
                : null;
        }
    }
}
