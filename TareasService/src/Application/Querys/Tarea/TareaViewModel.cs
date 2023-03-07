using System;
using System.Collections.Generic;
using OSPeConTI.Tareas.Domain.Entities;

namespace OSPeConTI.Tareas.Application.Queries
{
    public class TareaDTO
    {
        public TareaDTO()
        {
        }

        public Guid Id { get; set; }
        public Guid ReferenciaId { get; set; }
        public Sector Creador { get; set; }
        public Sector Ejecutor { get; set; }
        public DateTime Creacion { get; set; }
        public DateTime Vencimiento { get; set; }
        public DateTime VigenteDesde { get; set; }
        public int Alerta { get; set; }
        public string Descripcion { get; set; }
        public string Instrucciones { get; set; }
        public List<Link> Adjuntos { get; set; }
        public EstadoTarea Estado { get; set; }
        public TipoTarea Tipo { get; set; }
        public int Item { get; set; }
        public int Cantidad { get; set; }
        public int Lapso { get; set; }
        public List<TareaDTO> Consecuencias { get; set; }
    }

}