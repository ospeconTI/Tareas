using Microsoft.EntityFrameworkCore;
using OSPeConTI.Tareas.Domain.Entities;
using OSPeConTI.Tareas.Domain.SeedWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using OSPeConTI.Tareas.Domain.Repositories;
using System.Collections.Generic;

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

        public Sector Add(Sector sector)
        {
            return _context.Sectores.Add(sector).Entity;

        }
        public Sector Update(Sector sector)
        {
            return _context.Sectores.Update(sector).Entity;
        }

        public async Task<Sector> GetAsync(Guid id)
        {
            var item = await _context
                                .Sectores
                                .Include(i => i.Usuarios)
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

        public async Task<IEnumerable<Sector>> GetAllAsync()
        {
            var items = await _context.Sectores.Include(i => i.Usuarios).ToListAsync();

            if (items == null)
            {
                items = _context
                            .Sectores
                            .Local
                            .ToList();
            }

            return items;
        }

    }
}