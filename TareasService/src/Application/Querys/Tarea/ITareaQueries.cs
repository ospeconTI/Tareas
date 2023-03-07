namespace OSPeConTI.Tareas.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface ITareaQueries
    {
        Task<TareaDTO> GetAsync(Guid id);
        Task<IEnumerable<TareaDTO>> GetByIdReferenciaAsync(Guid id);


    }
}