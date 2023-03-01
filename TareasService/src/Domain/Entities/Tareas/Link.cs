using System;
using System.Collections.Generic;
using System.Linq;
using OSPeConTI.Tareas.Domain.Exceptions;
using OSPeConTI.Tareas.Domain.SeedWork;

namespace OSPeConTI.Tareas.Domain.Entities
{
    public class Link : Entity, IAggregateRoot
    {
        public Link()
        {
        }
        public Link(string url)
        {
            Url = url;
        }
        public string Url { get; private set; }

    }
}
