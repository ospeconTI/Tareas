using Microsoft.EntityFrameworkCore;
using OSPeConTI.ReferenciasService.Domain.Entities;
using OSPeConTI.ReferenciasService.Domain.SeedWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace OSPeConTI.ReferenciasService.Infrastructure.Repositories
{
    public class PlanRepository
        : IPlanRepository
    {
        private readonly ReferenciasContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public PlanRepository(ReferenciasContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Plan Add(Plan plan)
        {
            return _context.Planes.Add(plan).Entity;
        }
        public Plan Update(Plan plan)
        {
            return _context.Planes.Update(plan).Entity;
        }

        public async Task<Plan> GetAsync(Guid id)
        {
            var item = await _context
                                .Planes
                                .FirstOrDefaultAsync(o => o.Id == id);
            if (item == null)
            {
                item = _context
                            .Planes
                            .Local
                            .FirstOrDefault(o => o.Id == id);
            }

            return item;
        }

        public async Task<Plan> GetPlanByNameAsync(string descripcion)
        {
            var clasif = await _context
                    .Planes
                    .FirstOrDefaultAsync(o => o.Descripcion == descripcion);

            return clasif;
        }

        public Task<Plan> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }


    }
}