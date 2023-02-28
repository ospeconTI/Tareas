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
        [DataMember]
        public string CodigoSSS { get; set; }

        public AddSectorCommand()
        {

        }
        public AddSectorCommand(string descripcion, string codigoSSS)

        {
            Descripcion = descripcion;
            CodigoSSS = codigoSSS;
        }

    }
}