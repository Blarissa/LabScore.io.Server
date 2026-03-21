using AutoMapper;
using FluentValidation;
using LabScore.io.Server.Data.DTOs.Simulado;
using LabScore.io.Server.Model;
using LabScore.io.Server.Service;
using Microsoft.AspNetCore.Mvc;

namespace LabScore.io.Server.Controllers
{
    [Tags("Simulado")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class SimuladoController : ControllerBase
    {
        private readonly ISimuladoService _service;
        private readonly IMapper _mapper;
        private readonly IValidator<ResponderSimuladoCreateDto> _validator;

        public SimuladoController(ISimuladoService service, IMapper mapper, IValidator<ResponderSimuladoCreateDto> validator)
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Processa um simulado e retorna a nota final
        /// </summary>
        /// <remarks>
        /// Recebe a lista de questões respondidas, calcula a pontuação com base no gabarito e salva o resultado.
        /// </remarks>
        /// <param name="simuladoDto">Dados das respostas do usuário</param>
        /// <response code="201">Simulado processado e nota gerada com sucesso</response>
        /// <response code="400">Dados inválidos ou simulado vazio</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponderSimuladoReadDto))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ResponderSimuladoCreateDto simuladoDto)
        {
            var validationResult = await _validator.ValidateAsync(simuladoDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());

            var simuladoEntity = _mapper.Map<Simulado>(simuladoDto);
            var resultado = await _service.ProcessarSimuladoAsync(simuladoEntity);
            var simuladoRead = _mapper.Map<ResponderSimuladoReadDto>(resultado);

            return CreatedAtAction(nameof(Details), new { id = resultado.Id }, simuladoRead);
        }

        /// <summary>
        /// Obtém os detalhes de um simulado realizado
        /// </summary>
        /// <param name="id">Identificador único (UUID) do simulado</param>
        /// <response code="200">Detalhes do simulado encontrados</response>
        /// <response code="404">Simulado não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponderSimuladoReadDto))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details(Guid id)
        {
            var simulado = await _service.ObterPorIdAsync(id);

            return Ok(_mapper.Map<ResponderSimuladoReadDto>(simulado));
        }

        /// <summary>
        /// Lista o histórico de simulados (sem necessidade de login por enquanto)
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponderSimuladoReadDto>))]
        public async Task<IActionResult> All()
        {
            var lista = await _service.ListarTodosAsync();
            return Ok(_mapper.Map<List<ResponderSimuladoReadDto>>(lista));
        }

        /// <summary>
        /// Responde um simulado e retorna o resultado final.
        /// </summary>
        [HttpPost("responder")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponderSimuladoReadDto))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Responder([FromBody] ResponderSimuladoCreateDto simuladoDto)
            => Create(simuladoDto);
    }
}