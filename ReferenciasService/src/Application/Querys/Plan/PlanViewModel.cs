using System;
using System.Collections.Generic;
using OSPeConTI.ReferenciasService.Domain.Entities;

namespace OSPeConTI.ReferenciasService.Application.Queries
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