using MediatR;
using OSPeConTI.ReferenciasService.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace OSPeConTI.ReferenciasService.Application.Commands
{
    [DataContract]
    public class AddParentescoCommand : IRequest<Guid>
    {
         [DataMember]
         public string Descripcion { get; set; }
         [DataMember]        
        public string CodigoSSS { get; set; }

        public AddParentescoCommand()
        {

        }
        public AddParentescoCommand(string descripcion, string codigoSSS)

        {
            Descripcion = descripcion;          
            CodigoSSS=codigoSSS;
        }
    }
}