using System;
using System.Collections.Generic;
using OSPeConTI.Tareas.Domain.Entities;

namespace OSPeConTI.Tareas.Application.Queries
{
    public class SectorDTO
    {
        public SectorDTO()
        {
        }
        public Guid Id { get; set; }
        public string Descripcion { get; set; }


    }
}