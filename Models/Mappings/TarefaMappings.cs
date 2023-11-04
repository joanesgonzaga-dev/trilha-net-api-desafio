using AutoMapper;

namespace TrilhaApiDesafio.Models.Mappings
{
    public class TarefaMappings: Profile
    {
        public TarefaMappings()
        {
            CreateMap<TarefaWriteDTO, Tarefa>();
        } 
    }
}
