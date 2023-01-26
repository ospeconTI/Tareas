using MediatR;
using OSPeConTI.ReferenciasService.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace OSPeConTI.ReferenciasService.Application.Commands
{
    [DataContract]
    public class AddPlanCommand : IRequest<Guid>
    {
         [DataMember]
         public string Descripcion { get; set; }
         [DataMember]        
        public string CodigoSSS { get; set; }

        public AddPlanCommand()
        {

        }
        public AddPlanCommand(string descripcion, string codigoSSS)

        {
            Descripcion = descripcion;          
            CodigoSSS=codigoSSS;
        }
    }
}