using Microsoft.EntityFrameworkCore;
using OSPeConTI.ReferenciasService.Domain.Entities;
using OSPeConTI.ReferenciasService.Domain.SeedWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace OSPeConTI.ReferenciasService.Infrastructure.Repositories
{
    public class TipoDocumentoRepository
        : ITipoDocumentoRepository
    {
        private readonly ReferenciasContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public TipoDocumentoRepository(ReferenciasContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TipoDocumento Add(TipoDocumento tipoDocumento)
        {
            return _context.TipoDocumento.Add(tipoDocumento).Entity;
        }
        public TipoDocumento Update(TipoDocumento tipoDocumento)
        {
            return _context.TipoDocumento.Update(tipoDocumento).Entity;
        }

        public async Task<TipoDocumento> GetAsync(Guid id)
        {
            var item = await _context
                                .TipoDocumento
                                .FirstOrDefaultAsync(o => o.Id == id);
            if (item == null)
            {
                item = _context
                            .TipoDocumento
                            .Local
                            .FirstOrDefault(o => o.Id == id);
            }

            return item;
        }

        public async Task<TipoDocumento> GetTipoDocumentoByNameAsync(string descripcion)
        {
            var clasif = await _context
                    .TipoDocumento
                    .FirstOrDefaultAsync(o => o.Descripcion == descripcion);

            return clasif;
        }

        public Task<TipoDocumento> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }


    }
}