using Microsoft.EntityFrameworkCore;
using OSPeConTI.ReferenciasService.Domain.Entities;
using OSPeConTI.ReferenciasService.Domain.SeedWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace OSPeConTI.ReferenciasService.Infrastructure.Repositories
{
    public class ParentescoRepository
        : IParentescoRepository
    {
        private readonly ReferenciasContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ParentescoRepository(ReferenciasContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Parentesco Add(Parentesco parentesco)
        {
            return _context.Parentescos.Add(parentesco).Entity;
        }
        public Parentesco Update(Parentesco parentesco)
        {
            return _context.Parentescos.Update(parentesco).Entity;
        }

        public async Task<Parentesco> GetByIdAsync(Guid id)
        {
            var item = await _context
                                .Parentescos
                                .FirstOrDefaultAsync(o => o.Id == id);
            if (item == null)
            {
                item = _context
                            .Parentescos
                            .Local
                            .FirstOrDefault(o => o.Id == id);
            }

            return item;
        }

        public async Task<Parentesco> GetParentescoByNameAsync(string descripcion)
        {
            var clasif = await _context
                    .Parentescos
                    .FirstOrDefaultAsync(o => o.Descripcion == descripcion);

            return clasif;
        }

       

    }
}