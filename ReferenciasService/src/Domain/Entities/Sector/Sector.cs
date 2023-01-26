using System;
using System.Collections.Generic;
using System.Linq;


namespace OSPeConTI.Tareas.Domain.Entities
{
    public class Sector : Entity, IAggregateRoot
    {
        public Sector()
        {
        }

        public string Descripcion { get; private set; }
        public List<Guid> Usuarios { get; private set; }

    }
}
