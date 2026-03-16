using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabScore.io.Server.Model;

namespace LabScore.io.Server.Repository
{
    public interface IRepositoryQuestao : IRepository<Questao>{
        Task<IEnumerable<Questao>> CadastrarEmLoteAsync(IEnumerable<Questao> questoes);
    }
}
