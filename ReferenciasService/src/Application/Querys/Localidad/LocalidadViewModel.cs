using System;
using System.Collections.Generic;
using OSPeConTI.ReferenciasService.Domain.Entities;

namespace OSPeConTI.ReferenciasService.Application.Queries
{
    public class LocalidadDTO
    {
        public LocalidadDTO()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000000");
            Descripcion = "";
            ProvinciasId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            ProvinciaNombre = "";
            CodigoPostal="";
            CodigoSSS = "";
        }
        public Guid Id { get; set; }
         public string Descripcion { get; set; }
        public Guid ProvinciasId { get; set; }
        public string ProvinciaNombre { get; set; }
        public string CodigoPostal { get; set; }
        public string CodigoSSS { get; set; }

    }
}