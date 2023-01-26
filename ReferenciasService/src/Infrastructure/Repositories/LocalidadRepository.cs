using Microsoft.EntityFrameworkCore;
using OSPeConTI.ReferenciasService.Domain.Entities;
using OSPeConTI.ReferenciasService.Domain.SeedWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace OSPeConTI.ReferenciasService.Infrastructure.Repositories
{
    public class LocalidadRepository
        : ILocalidadRepository
    {
        private readonly ReferenciasContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public LocalidadRepository(ReferenciasContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Localidad Add(Localidad localidad)
        {
            return _context.Localidades.Add(localidad).Entity;
        }
        public Localidad Update(Localidad localidad)
        {
            return _context.Localidades.Update(localidad).Entity;
        }

        public async Task<Localidad> GetAsync(Guid id)
        {
            var item = await _context
                                .Localidades
                                .FirstOrDefaultAsync(o => o.Id == id);
            if (item == null)
            {
                item = _context
                            .Localidades
                            .Local
                            .FirstOrDefault(o => o.Id == id);
            }

            return item;
        }

        public async Task<Localidad> GetLocalidadByNameAsync(string descripcion)
        {
            var clasif = await _context
                    .Localidades
                    .FirstOrDefaultAsync(o => o.Descripcion == descripcion);

            return clasif;
        }

        public Task<Localidad> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Localidad> GetLocalidadByProvinciaAsync(Guid provinciaId)
        {
            throw new NotImplementedException();
        }

    }
}