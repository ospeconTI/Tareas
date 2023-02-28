namespace OSPeConTI.Tareas.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface IProvinciaQueries
    {
        Task<ProvinciaDTO> GetProvinciaAsync(Guid id);
        Task<IEnumerable<ProvinciaDTO>> GetProvinciaByNameAsync(string descripcion);
        Task<IEnumerable<ProvinciaDTO>> GetAll();

    }
}