using Microsoft.EntityFrameworkCore;
using OSPeConTI.Tareas.Domain.Entities;
using OSPeConTI.Tareas.Domain.SeedWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using OSPeConTI.Tareas.Domain.Repositories;

namespace OSPeConTI.Tareas.Infrastructure.Repositories
{
    public class SectorRepository
        : ISectorRepository
    {
        private readonly TareasContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public SectorRepository(TareasContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Sector Add(Sector Sector)
        {
            return _context.Sectores.Add(Sector).Entity;
        }
        public Sector Update(Sector Sector)
        {
            return _context.Sectores.Update(Sector).Entity;
        }

        public async Task<Sector> GetAsync(Guid id)
        {
            var item = await _context
                                .Sectores
                                .FirstOrDefaultAsync(o => o.Id == id);
            if (item == null)
            {
                item = _context
                            .Sectores
                            .Local
                            .FirstOrDefault(o => o.Id == id);
            }

            return item;
        }

        public async Task<Sector> GetSectorByNameAsync(string descripcion)
        {
            var clasif = await _context
                    .Sectores
                    .FirstOrDefaultAsync(o => o.Descripcion == descripcion);

            return clasif;
        }

        public Task<Sector> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }


    }
}