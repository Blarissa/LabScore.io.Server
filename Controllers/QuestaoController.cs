using AutoMapper;
using FluentValidation;
using LabScore.io.Server.Data.DTOs.Questao;
using LabScore.io.Server.Data.DTOs.Simulado;
using LabScore.io.Server.Model;
using LabScore.io.Server.Repository;
using LabScore.io.Server.Service;
using Microsoft.AspNetCore.Mvc;

namespace LabScore.io.Server.Controllers
{
    /// <summary>
    /// Endpoints para gerenciamento de questões.
    /// </summary>
    [Tags("Questão")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class QuestaoController : ControllerBase
    {
        private readonly IQuestaoService _service;
        private readonly IMapper _mapper;
        private readonly IValidator<Questao> _validator;
        private readonly IValidator<List<QuestaoCreateDto>> _bulkValidator;

        /// <summary>
        /// Inicializa o controller de questões.
        /// </summary>
        public QuestaoController(
            IQuestaoService service,
            IMapper mapper,
            IValidator<Questao> validator,
            IValidator<List<QuestaoCreateDto>> bulkValidator)
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
            _bulkValidator = bulkValidator;
        }

        /// <summary>
        /// Lista todas as questões disponíveis no banco
        /// </summary>
        /// <response code="200">Retorna a lista de questões com alternativas</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<QuestaoReadDto>))]
        public async Task<IActionResult> All()
        {
            var questoes = await _service.RecuperarTodasAsync();
            var questoesDto = _mapper.Map<List<QuestaoReadDto>>(questoes);

            return Ok(questoesDto);
        }

        /// <summary>
        /// Obtém os detalhes de uma questão específica por UUID
        /// </summary>
        /// <param name="id">Identificador único da questão</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestaoReadDto))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details(Guid id)
        {
            var questao = await _service.RecuperarPorIdAsync(id);

            return Ok(_mapper.Map<QuestaoReadDto>(questao));
        }

        /// <summary>
        /// Cadastra uma nova questão no laboratório
        /// </summary>
        /// <remarks>
        /// Ao cadastrar, envie a lista de alternativas e o ID da que for correta.
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(QuestaoReadDto))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] QuestaoCreateDto questaoDto)
        {
            var questaoEntity = _mapper.Map<Questao>(questaoDto);

            var validationResult = await _validator.ValidateAsync(questaoEntity);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());

            var questaoCriada = await _service.CadastrarAsync(questaoEntity);
            var questaoRead = _mapper.Map<QuestaoReadDto>(questaoCriada);

            return CreatedAtAction(nameof(Details), new { id = questaoCriada.Id }, questaoRead);
        }

        /// <summary>
        /// Cadastra várias questões no laboratório
        /// </summary>
        /// <param name="questoesDto"></param>
        /// <returns>
        /// Retorna a lista de questões cadastradas
        /// </returns>
        [HttpPost("create-bulk")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<QuestaoReadDto>))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBulk([FromBody] List<QuestaoCreateDto> questoesDto)
        {
            var bulkValidation = await _bulkValidator.ValidateAsync(questoesDto);
            if (!bulkValidation.IsValid)
                return BadRequest(bulkValidation.ToDictionary());

            var questoesEntity = _mapper.Map<List<Questao>>(questoesDto);

            foreach (var q in questoesEntity)
            {
                var result = await _validator.ValidateAsync(q);
                if (!result.IsValid)
                    return BadRequest(
                        new { mensagem = "Erro em uma das questões do lote", erros = result.ToDictionary() });
            }

            await _service.CadastrarEmLoteAsync(questoesEntity);

            var questoesRead = _mapper.Map<List<QuestaoReadDto>>(questoesEntity);
            return CreatedAtAction(nameof(All), questoesRead);
        }

        /// <summary>
        /// Monta um conjunto de questões para aplicação de simulado.
        /// </summary>
        /// <remarks>
        /// Recebe uma lista de IDs de questões e retorna o pacote pronto para exibição ao participante.
        /// </remarks>
        /// <response code="200">Conjunto montado com sucesso</response>
        /// <response code="400">Lista de questões inválida</response>
        /// <response code="404">Uma ou mais questões não encontradas</response>
        [HttpPost("conjunto")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SimuladoConjuntoReadDto))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CriarConjunto([FromBody] SimuladoConjuntoCreateDto dto)
        {
            if (dto?.QuestoesIds == null || dto.QuestoesIds.Count == 0)
                return BadRequest(new { mensagem = "Informe ao menos um ID de questão." });

            var ids = dto.QuestoesIds.Distinct().ToList();

            var questoes = new List<Questao>();

            // Busca sequencial para evitar acesso concorrente ao mesmo DbContext
            foreach (var id in ids)
            {
                var q = await _service.RecuperarPorIdAsync(id);
                if (q is not null)
                    questoes.Add(q);
            }

            if (questoes.Count == 0)
                return NotFound(new { mensagem = "Nenhuma questão encontrada para os IDs informados." });

            var retorno = new SimuladoConjuntoReadDto
            {
                TotalQuestoes = questoes.Count,
                Questoes = _mapper.Map<List<QuestaoReadDto>>(questoes)
            };
            return Ok(retorno);
        }
    }
}
