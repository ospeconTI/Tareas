using System;
using System.Collections.Generic;
using System.Linq;
using OSPeConTI.Tareas.Domain.Exceptions;
using OSPeConTI.Tareas.Domain.SeedWork;

namespace OSPeConTI.Tareas.Domain.Entities
{
    public class Usuario : Entity, IAggregateRoot
    {
        public Usuario()
        {
        }
        public Usuario(string apellido, string nombre, string email, Guid identificacion)
        {
            Apellido = apellido;
            Nombre = nombre;
            Email = email;
            Identificacion = identificacion;

        }
        public string Apellido { get; private set; }
        public string Nombre { get; private set; }
        public string Email { get; private set; }
        public Guid Identificacion { get; private set; }

    }
}
