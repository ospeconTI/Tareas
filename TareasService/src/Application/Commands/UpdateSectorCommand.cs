using MediatR;
using OSPeConTI.Tareas.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace OSPeConTI.Tareas.Application.Commands
{
    [DataContract]
    public class UpdateSectorCommand : IRequest<bool>
    {
        [DataMember]
        public string Descripcion { get; set; }

        public UpdateSectorCommand()
        {

        }
        public UpdateSectorCommand(string descripcion)

        {
            Descripcion = descripcion;
        }

    }
}