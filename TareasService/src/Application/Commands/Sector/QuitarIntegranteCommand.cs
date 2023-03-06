using MediatR;
using OSPeConTI.Tareas.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace OSPeConTI.Tareas.Application.Commands
{
    [DataContract]
    public class QuitarIntegranteCommand : IRequest<bool>
    {

        [DataMember]
        public Guid SectorId { get; set; }

        [DataMember]
        public Guid Identificador { get; set; }


        public QuitarIntegranteCommand()
        {

        }
        public QuitarIntegranteCommand(Guid sectorId, Guid identificador)

        {

            SectorId = sectorId;

            Identificador = identificador;

        }

    }
}