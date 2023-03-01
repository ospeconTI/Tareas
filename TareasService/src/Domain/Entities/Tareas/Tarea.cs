using System;
using System.Collections.Generic;
using System.Linq;
using OSPeConTI.Tareas.Domain.Exceptions;
using OSPeConTI.Tareas.Domain.SeedWork;

namespace OSPeConTI.Tareas.Domain.Entities
{
    public class Tarea : Entity, IAggregateRoot
    {

        public Guid ReferenciaId { get; private set; }
        public Sector Creador { get; private set; }
        public Sector Ejecutor { get; private set; }
        public DateTime Creacion { get; private set; }
        public DateTime Vencimiento { get; private set; }
        public DateTime VigenteDesde { get; private set; }
        public int Alerta { get; private set; }
        public string Descripcion { get; private set; }
        public string Instrucciones { get; private set; }
        public List<Link> Adjuntos { get; private set; }
        public EstadoTarea Estado { get; private set; }
        public TipoTarea Tipo { get; private set; }
        public List<Tarea> Consecuencias { get; private set; }

        public Tarea() { }
        public Tarea(Guid referenciaId, Sector creador, Sector ejecutor, DateTime vigenteDesde, DateTime vencimiento, int alerta, string descripcion, string instrucciones, TipoTarea tipoTarea)
        {

            if (vigenteDesde < DateTime.Now) throw new TareaDomainException("La Tarea no puede estar vigente antes del día de su creación");
            if (Vencimiento < vigenteDesde.AddDays(alerta)) throw new TareaDomainException("No puede crear una tarea vencida");
            if (descripcion == string.Empty) throw new TareaDomainException("La descripción de la tarea no puede estar vacia");

            ReferenciaId = referenciaId;
            Creador = creador;
            Ejecutor = ejecutor;
            Creacion = DateTime.Now;
            VigenteDesde = vigenteDesde;
            Vencimiento = vencimiento;
            Alerta = alerta;
            Descripcion = descripcion;
            Instrucciones = instrucciones;
            Tipo = tipoTarea;

        }

        public void DarVencimiento()
        {
            Estado = EstadoTarea.Vencida;
        }
        public void DarCumplimiento()
        {
            Estado = EstadoTarea.Cumplida;
        }
        public void Postergar(int Dias)
        {
            Estado = EstadoTarea.Pendiente;
        }
        public void Anular(string Motivo)
        {
            Estado = EstadoTarea.Anulada;
        }
        public void Pausar(string Motivo)
        {
            Estado = EstadoTarea.Pausada;
        }
        public void SumarConsecuencia(Tarea tarea)
        {

            if (this.Tipo == TipoTarea.Compleja) throw new TareaDomainException("Solo se pueden agregar tareas a tareas complejas");
            if (tarea.Consecuencias.Contains(tarea)) throw new TareaDomainException("La tarea ya fue asignada");

            this.Consecuencias.Add(tarea);
        }
        public void QuitarConsecuencia(Tarea tarea)
        {
            if (Estado == EstadoTarea.Cumplida || Estado == EstadoTarea.Anulada) throw new TareaDomainException("No se puede remover porque la tarea ha finalizado");
            this.Consecuencias.Remove(tarea);
        }
    }
}