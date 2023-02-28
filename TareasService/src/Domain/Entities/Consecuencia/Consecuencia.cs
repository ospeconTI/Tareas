using System;
using System.Collections.Generic;
using System.Linq;
using OSPeConTI.Tareas.Domain.SeedWork;
using OSPeConTI.Tareas.Domain.Exceptions;

namespace OSPeConTI.Tareas.Domain.Entities
{
    public class Consecuencia : Entity, IAggregateRoot
    {
        public Consecuencia()
        {
        }
        public Guid IdTarea { get; private set; }
        public EstadoTarea Estado { get; private set; }

    }
}