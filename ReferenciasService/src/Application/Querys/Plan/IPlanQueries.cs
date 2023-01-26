namespace OSPeConTI.ReferenciasService.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    

    public interface IPlanQueries
    {
        Task<PlanDTO> GetPlanAsync(Guid id);
        Task<IEnumerable<PlanDTO>> GetPlanByNameAsync(string descripcion);
        Task<IEnumerable<PlanDTO>> GetAll();

    }
}