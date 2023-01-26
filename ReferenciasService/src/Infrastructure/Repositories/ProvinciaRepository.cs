using Microsoft.EntityFrameworkCore;
using OSPeConTI.ReferenciasService.Domain.Entities;
using OSPeConTI.ReferenciasService.Domain.SeedWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace OSPeConTI.ReferenciasService.Infrastructure.Repositories
{
    public class ProvinciaRepository
        : IProvinciaRepository
    {
        private readonly ReferenciasContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ProvinciaRepository(ReferenciasContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Provincia Add(Provincia provincia)
        {
            return _context.Provincias.Add(provincia).Entity;
        }
        public Provincia Update(Provincia provincia)
        {
            return _context.Provincias.Update(provincia).Entity;
        }

        public async Task<Provincia> GetAsync(Guid id)
        {
            var item = await _context
                                .Provincias
                                .FirstOrDefaultAsync(o => o.Id == id);
            if (item == null)
            {
                item = _context
                            .Provincias
                            .Local
                            .FirstOrDefault(o => o.Id == id);
            }

            return item;
        }

        public async Task<Provincia> GetProvinciaByNameAsync(string descripcion)
        {
            var clasif = await _context
                    .Provincias
                    .FirstOrDefaultAsync(o => o.Descripcion == descripcion);

            return clasif;
        }

        public Task<Provincia> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }


    }
}