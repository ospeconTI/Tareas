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
    public class ConsecuenciaRepository
        : IConsecuenciaRepository
    {
        private readonly TareasContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ConsecuenciaRepository(TareasContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Consecuencia Add(Consecuencia Consecuencia)
        {
            return _context.Consecuencias.Add(Consecuencia).Entity;
        }
        public Consecuencia Update(Consecuencia Consecuencia)
        {
            return _context.Consecuencias.Update(Consecuencia).Entity;
        }

        public async Task<Consecuencia> GetAsync(Guid id)
        {
            var item = await _context
                                .Consecuencias
                                .FirstOrDefaultAsync(o => o.Id == id);
            if (item == null)
            {
                item = _context
                            .Consecuencias
                            .Local
                            .FirstOrDefault(o => o.Id == id);
            }

            return item;
        }

        public async Task<Consecuencia> GetConsecuenciaByNameAsync(string descripcion)
        {
            throw new NotImplementedException();
        }

        public Task<Consecuencia> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }


    }
}