namespace OSPeConTI.Tareas.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface IParentescoQueries
    {
        Task<ParentescoDTO> GetParentescoAsync(Guid id);
        Task<IEnumerable<ParentescoDTO>> GetParentescoByNameAsync(string descripcion);
        Task<IEnumerable<ParentescoDTO>> GetAll();

    }
}