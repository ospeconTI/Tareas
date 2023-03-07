using MediatR;
using OSPeConTI.Tareas.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace OSPeConTI.Tareas.Application.Commands
{
    [DataContract]
    public class CrearSimpleCommand : IRequest<Guid>
    {
        [DataMember]
        public Guid ReferenciaId { get; set; }
        [DataMember]
        public Sector Creador { get; set; }
        [DataMember]
        public Sector Ejecutor { get; set; }
        [DataMember]
        public DateTime Creacion { get; set; }
        [DataMember]
        public int VenceEn { get; set; }
        [DataMember]
        public DateTime VigenteDesde { get; set; }
        [DataMember]
        public int Alerta { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Instrucciones { get; set; }


        public CrearSimpleCommand()
        {

        }
        public CrearSimpleCommand(Guid referenciaId, Sector creador, Sector ejecutor, DateTime vigenteDesde, int venceEn, int alerta, string descripcion, string instrucciones)

        {
            ReferenciaId = referenciaId;
            Creador = creador;
            Ejecutor = ejecutor;
            VigenteDesde = vigenteDesde;
            VenceEn = venceEn;
            Alerta = alerta;
            Descripcion = descripcion;
            Instrucciones = instrucciones;
        }

    }
}