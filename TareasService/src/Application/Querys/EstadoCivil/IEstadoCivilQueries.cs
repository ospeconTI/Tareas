namespace OSPeConTI.Tareas.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface IEstadoCivilQueries
    {
        Task<EstadoCivilDTO> GetEstadoCivilAsync(Guid id);
        Task<IEnumerable<EstadoCivilDTO>> GetEstadoCivilByNameAsync(string descripcion);
        Task<IEnumerable<EstadoCivilDTO>> GetAll();

    }
}