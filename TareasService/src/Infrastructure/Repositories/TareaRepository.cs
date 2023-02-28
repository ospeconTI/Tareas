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

        public Tarea Add(Tarea Tarea)
        {
            return _context.Tareas.Add(Tarea).Entity;
        }
        public Tarea Update(Tarea Tarea)
        {
            return _context.Tareas.Update(Tarea).Entity;
        }

        public async Task<Tarea> GetAsync(Guid id)
        {
            var item = await _context
                                .Tareas
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

        public async Task<Tarea> GetTareaByNameAsync(string descripcion)
        {
            var clasif = await _context
                    .Tareas
                    .FirstOrDefaultAsync(o => o.Descripcion == descripcion);

            return clasif;
        }

        public Task<Tarea> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Tarea> GetTareaByProvinciaAsync(Guid provinciaId)
        {
            throw new NotImplementedException();
        }

    }
}