namespace OSPeConTI.Tareas.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface ITipoDocumentoQueries
    {
        Task<TipoDocumentoDTO> GetTipoDocumentoAsync(Guid id);
        Task<IEnumerable<TipoDocumentoDTO>> GetTipoDocumentoByNameAsync(string descripcion);
        Task<IEnumerable<TipoDocumentoDTO>> GetAll();

    }
}