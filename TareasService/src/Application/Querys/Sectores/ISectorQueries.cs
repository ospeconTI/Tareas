namespace OSPeConTI.Tareas.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface ISectorQueries
    {
        Task<SectorDTO> GetAsync(Guid id);
        Task<IEnumerable<SectorDTO>> GetByDescripcionAsync(string descripcion);
        Task<IEnumerable<SectorDTO>> GetAll();

    }
}