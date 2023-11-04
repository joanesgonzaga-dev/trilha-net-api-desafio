using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Services
{
    public class TarefaServices : ITarefaServices
    {
        OrganizadorContext _context;
        IMapper _mapper;
        public TarefaServices(OrganizadorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<List<Tarefa>> ObterTodos()
        {
            return await _context.Tarefas.ToListAsync();
        }

        public async Task<Tarefa> BuscarPorData(DateTime data)
        {
            try
            {
                var tarefa = await _context.Tarefas.Where(x => x.Data.Date == data.Date).FirstOrDefaultAsync();
                return tarefa;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Tarefa> BuscarPorId(int id)
        {
            try
            {
                Tarefa tarefa = await _context.Tarefas.AsNoTracking().SingleOrDefaultAsync(t => t.Id == id);
                return tarefa;
            }
            catch (ArgumentNullException nullEx)
            {

                throw nullEx;
            }

            catch (InvalidOperationException invalidEx) 
            {
                throw invalidEx;
            }

            catch (Exception)
            {
                throw;
            }
           
        }

        public async Task CriarTarefa(Tarefa tarefa)
        {
            try
            {
                _context.Add(tarefa);
               await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Tarefa> BuscarPorTitulo(string titulo)
        {
            try
            {
                var tarefa = await _context.Tarefas.AsNoTracking().Where(t => t.Titulo == titulo).SingleOrDefaultAsync();
                return tarefa;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Tarefa> BuscarPorStatus(EnumStatusTarefa status)
        {
            try
            {
                var tarefa = await _context.Tarefas.AsNoTracking().Where(t => t.Status == status).SingleOrDefaultAsync();
                return tarefa;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task AtualizarTarefa(int id, Tarefa tarefa)
        {
            try
            {
                tarefa.Id = id;
                _context.Tarefas.Update(tarefa);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task DeletarTarefa(Tarefa tarefa)
        {
            try
            {
                _context.Remove(tarefa);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
