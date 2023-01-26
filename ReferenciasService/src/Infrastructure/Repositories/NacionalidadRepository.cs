using Microsoft.EntityFrameworkCore;
using OSPeConTI.ReferenciasService.Domain.Entities;
using OSPeConTI.ReferenciasService.Domain.SeedWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace OSPeConTI.ReferenciasService.Infrastructure.Repositories
{
    public class NacionalidadRepository
        : INacionalidadRepository
    {
        private readonly ReferenciasContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public NacionalidadRepository(ReferenciasContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Nacionalidad Add(Nacionalidad nacionalidad)
        {
            return _context.Nacionalidades.Add(nacionalidad).Entity;
        }
        public Nacionalidad Update(Nacionalidad nacionalidad)
        {
            return _context.Nacionalidades.Update(nacionalidad).Entity;
        }

        public async Task<Nacionalidad> GetAsync(Guid id)
        {
            var item = await _context
                                .Nacionalidades
                                .FirstOrDefaultAsync(o => o.Id == id);
            if (item == null)
            {
                item = _context
                            .Nacionalidades
                            .Local
                            .FirstOrDefault(o => o.Id == id);
            }

            return item;
        }

        public async Task<Nacionalidad> GetNacionalidadByNameAsync(string descripcion)
        {
            var clasif = await _context
                    .Nacionalidades
                    .FirstOrDefaultAsync(o => o.Descripcion == descripcion);

            return clasif;
        }

        public Task<Nacionalidad> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }


    }
}