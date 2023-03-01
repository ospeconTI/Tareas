using Microsoft.EntityFrameworkCore;
using OSPeConTI.Tareas.Domain.Entities;
using OSPeConTI.Tareas.Domain.SeedWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using OSPeConTI.Tareas.Domain.Repositories;

namespace OSPeConTI.Tareas.Infrastructure.Repositories
{
    public class TareaRepository
        : ITareaRepository
    {
        private readonly TareasContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public TareaRepository(TareasContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Tarea Add(Tarea tarea)
        {
            return _context.Tareas.Add(tarea).Entity;
        }

        public Tarea Update(Tarea tarea)
        {
            return _context.Tareas.Update(tarea).Entity;
        }

        public async Task<Tarea> GetAsync(Guid id)
        {
            var item = await _context
                                .Tareas
                                .Include(i => i.Consecuencias)
                                .FirstOrDefaultAsync(o => o.Id == id);
            if (item == null)
            {
                item = _context
                            .Tareas
                            .Local
                            .FirstOrDefault(o => o.Id == id);
            }

            return item;
        }

        public async Task<IEnumerable<Tarea>> GetByReferenciaAsync(Guid referenciaId)
        {
            var items = await _context.Tareas.Include(i => i.Consecuencias).Where(w => w.ReferenciaId == referenciaId).ToListAsync();

            if (items == null)
            {
                items = _context
                            .Tareas
                            .Local
                            .Where(w => w.ReferenciaId == referenciaId)
                            .ToList();
            }

            return items;
        }
    }
}