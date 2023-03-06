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
        public List<Usuario> Usuarios { get; private set; }

        public void Update(string descripcion)
        {
            if (descripcion == string.Empty) throw new SectorDomainException("La descripcion no puede quedar vacia");
            Descripcion = descripcion;

        }



        public void SumarIntegrante(Usuario usuario)
        {
            if (usuario.Identificacion == Guid.Empty) throw new SectorDomainException("El usuario Debe tener un Identificador");
            if (usuario.Apellido == string.Empty) throw new SectorDomainException("El usuario Debe tener Apellido");
            if (usuario.Email == string.Empty) throw new SectorDomainException("El usuario Debe tener email");

            if (Usuarios == null) Usuarios = new List<Usuario>();


            if (Usuarios.FirstOrDefault(u => u.Identificacion == usuario.Identificacion) != null) throw new SectorDomainException("El usuario ya existe en el sector");

            Usuarios.Add(usuario);
        }

        public void QuitarIntegrante(Usuario usuario)
        {
            if (usuario == null) throw new SectorDomainException("Debe especificar un usuario");

            Usuarios.Remove(usuario);
        }

    }
}
