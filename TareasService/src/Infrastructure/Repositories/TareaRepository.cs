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
            throw new NotImplementedException();
        }

        public Tarea Update(Tarea tarea)
        {
            throw new NotImplementedException();
        }

        public Task<Tarea> GetAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tarea>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}