namespace OSPeConTI.ReferenciasService.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    

    public interface ILocalidadQueries
    {
        Task<LocalidadDTO> GetLocalidadAsync(Guid id);
        Task<IEnumerable<LocalidadDTO>> GetLocalidadByNameAsync(string descripcion);
        Task<IEnumerable<LocalidadDTO>> GetAll();
        Task<List<LocalidadDTO>> GetLocalidadByProvincia(Guid provinciaId);


    }
}