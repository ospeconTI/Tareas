using Microsoft.EntityFrameworkCore;
using OSPeConTI.ReferenciasService.Domain.Entities;
using OSPeConTI.ReferenciasService.Domain.SeedWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace OSPeConTI.ReferenciasService.Infrastructure.Repositories
{
    public class EstadoCivilRepository
        : IEstadoCivilRepository
    {
        private readonly ReferenciasContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public EstadoCivilRepository(ReferenciasContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public EstadoCivil Add(EstadoCivil estadoCivil)
        {
            return _context.EstadosCiviles.Add(estadoCivil).Entity;
        }
        public EstadoCivil Update(EstadoCivil estadoCivil)
        {
            return _context.EstadosCiviles.Update(estadoCivil).Entity;
        }

        public async Task<EstadoCivil> GetAsync(Guid id)
        {
            var item = await _context
                                .EstadosCiviles
                                .FirstOrDefaultAsync(o => o.Id == id);
            if (item == null)
            {
                item = _context
                            .EstadosCiviles
                            .Local
                            .FirstOrDefault(o => o.Id == id);
            }

            return item;
        }

        public async Task<EstadoCivil> GetEstadoCivilByNameAsync(string descripcion)
        {
            var clasif = await _context
                    .EstadosCiviles
                    .FirstOrDefaultAsync(o => o.Descripcion == descripcion);

            return clasif;
        }

        public Task<EstadoCivil> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }


    }
}