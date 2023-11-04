using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Services;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaServices _services;
        private readonly IMapper _mapper;

        public TarefaController(ITarefaServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            var tarefa = await _services.BuscarPorId(id);
            if (tarefa is null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            var lista = await _services.ObterTodos();

            if (lista is null)
            {
                return NoContent();
            }

            return Ok(lista);
        }

        [HttpGet("ObterPorTitulo")]
        public async Task<IActionResult> ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefa = await _services.BuscarPorTitulo(titulo);
            if (tarefa is null)
            {
                return NotFound();
            }
            
            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public async Task<IActionResult> ObterPorData(DateTime data)
        {
            var tarefa = await _services.BuscarPorData(data);
            if (tarefa is null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public async Task<IActionResult> ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefa = await _services.BuscarPorStatus(status);
            if (tarefa is null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            await _services.CriarTarefa(tarefa);
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, TarefaWriteDTO tarefa)
        {
            var tarefaBanco = await _services.BuscarPorId(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            
            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            tarefaBanco = _mapper.Map<Tarefa>(tarefa);
            await _services.AtualizarTarefa(id, tarefaBanco);
            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarTarefa(int id)
        {
            var tarefaBanco = await _services.BuscarPorId(id);

            if (tarefaBanco == null)
                return NotFound();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            await _services.DeletarTarefa(tarefaBanco);
            return NoContent();
        }
    }
}
