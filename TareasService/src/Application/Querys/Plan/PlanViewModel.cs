using System;
using System.Collections.Generic;
using OSPeConTI.Tareas.Domain.Entities;

namespace OSPeConTI.Tareas.Application.Queries
{
    public class PlanDTO
    {
        public PlanDTO()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000000");
            Descripcion = "SIN CLASIFICACION";
            CodigoSSS = "";
        }
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public string CodigoSSS { get; set; }

    }
}