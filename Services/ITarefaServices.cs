using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Services
{
    public interface ITarefaServices
    {
        Task<List<Tarefa>> ObterTodos();
        Task<Tarefa> BuscarPorId(int id);
        Task<Tarefa> BuscarPorData(DateTime data);
        Task<Tarefa> BuscarPorTitulo(string titulo);
        Task<Tarefa> BuscarPorStatus(EnumStatusTarefa status);
        Task CriarTarefa(Tarefa tarefa);
        Task AtualizarTarefa(int id, Tarefa tarefa);
        Task DeletarTarefa(Tarefa tarefa);
    }
}
