using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OSPeConTI.Tareas.Domain.Entities;

namespace OSPeConTI.Tareas.Domain.Repositories
{
    public interface ITareaRepository
    {
        Tarea Add(Tarea tarea);
        Tarea Update(Tarea tarea);
        Task<Tarea> GetAsync(Guid Id);
        Task<IEnumerable<Tarea>> GetAllAsync();

    }
}