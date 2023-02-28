using System;
using System.Collections.Generic;
using System.Linq;
using OSPeConTI.Tareas.Domain.Exceptions;
using OSPeConTI.Tareas.Domain.SeedWork;

namespace OSPeConTI.Tareas.Domain.Entities
{
    public class Sector : Entity, IAggregateRoot
    {
        public Sector()
        {
        }

        public Sector(string descripcion)
        {
            if (descripcion == String.Empty) throw new SectorDomainException("La descricpion no puede quedar vacia");
            Descripcion = descripcion;
        }

        public string Descripcion { get; private set; }
        public List<Guid> Usuarios { get; private set; }

        public void sumarIntegrante(Guid usuario)
        {
            if (Usuarios == null) Usuarios = new List<Guid>();
            if (Usuarios.Find(u => u == usuario) != null) throw new SectorDomainException("El usuario ya existe en el sector");
            Usuarios.Add(usuario);
        }

    }
}
