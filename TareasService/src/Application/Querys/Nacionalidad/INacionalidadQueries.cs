namespace OSPeConTI.Tareas.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface INacionalidadQueries
    {
        Task<NacionalidadDTO> GetNacionalidadAsync(Guid id);
        Task<IEnumerable<NacionalidadDTO>> GetNacionalidadByNameAsync(string descripcion);
        Task<IEnumerable<NacionalidadDTO>> GetAll();

    }
}