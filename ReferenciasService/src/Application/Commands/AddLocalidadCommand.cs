using MediatR;
using OSPeConTI.ReferenciasService.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace OSPeConTI.ReferenciasService.Application.Commands
{
    [DataContract]
    public class AddLocalidadCommand : IRequest<Guid>
    {
         [DataMember]
         public string Descripcion { get; set; }
         [DataMember]        
        public Guid ProvinciaId { get; set; }
         [DataMember]        
         public string CodigoPostal { get; set; }
         [DataMember]        
        public string CodigoSSS { get; set; }
        [DataMember]        
        public Provincia Provincia { get; set; }

        

        public AddLocalidadCommand()
        {

        }
        public AddLocalidadCommand(string descripcion, Guid provinciaId, string codigoPostal, string codigoSSS)
        {
            Descripcion = descripcion;
            ProvinciaId = provinciaId;
            CodigoPostal = codigoPostal;
            CodigoSSS = codigoSSS;
        }
    }
}