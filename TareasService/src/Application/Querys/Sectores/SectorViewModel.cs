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
        public List<UsuarioDTO> Usuarios { get; set; }


    }
    public class UsuarioDTO
    {
        public UsuarioDTO()
        {
        }

        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }


    }
}