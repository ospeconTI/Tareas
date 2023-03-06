using MediatR;
using OSPeConTI.Tareas.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace OSPeConTI.Tareas.Application.Commands
{
    [DataContract]
    public class AddSectorCommand : IRequest<Guid>
    {
        [DataMember]
        public string Descripcion { get; set; }


        public AddSectorCommand()
        {

        }
        public AddSectorCommand(string descripcion)

        {
            Descripcion = descripcion;

        }

    }
}