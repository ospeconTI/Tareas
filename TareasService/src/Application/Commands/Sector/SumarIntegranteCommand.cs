using MediatR;
using OSPeConTI.Tareas.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace OSPeConTI.Tareas.Application.Commands
{
    [DataContract]
    public class SumarIntegranteCommand : IRequest<bool>
    {

        [DataMember]
        public Guid SectorId { get; set; }
        [DataMember]
        public string Apellido { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public Guid Identificador { get; set; }


        public SumarIntegranteCommand()
        {

        }
        public SumarIntegranteCommand(Guid sectorId, string apellido, string nombre, string email, Guid identificador)

        {

            SectorId = sectorId;
            Apellido = apellido;
            Nombre = nombre;
            Email = email;
            Identificador = identificador;

        }

    }
}