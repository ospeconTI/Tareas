using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OSPeConTI.Tareas.Domain.Entities;

namespace OSPeConTI.Tareas.Domain.Repositories
{
    public interface ISectorRepository
    {
        Sector Add(Sector sector);
        Sector Update(Sector sector);
        Task<Sector> GetAsync(Guid Id);
        Task<IEnumerable<Sector>> GetAllAsync();

    }
}