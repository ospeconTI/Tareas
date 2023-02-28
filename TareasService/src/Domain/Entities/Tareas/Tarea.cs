using System;
using System.Collections.Generic;
using System.Linq;
using OSPeConTI.Tareas.Domain.Exceptions;
using OSPeConTI.Tareas.Domain.SeedWork;

namespace OSPeConTI.Tareas.Domain.Entities
{
    public class Tarea : Entity, IAggregateRoot
    {

        public Guid IdReferencia { get; private set; }
        public Guid IdCreador { get; private set; }
        public Guid IdEjecutor { get; private set; }
        public Sector Creador { get; private set; }
        public Sector Ejecutor { get; private set; }
        public DateTime Creacion { get; private set; }
        public DateTime Vencimiento { get; private set; }
        public DateTime VigenteDesde { get; private set; }
        public int Alerta { get; private set; }
        public string Descripcion { get; private set; }
        public string Instrucciones { get; private set; }
        public List<string> Adjuntos { get; private set; }
        public EstadoTarea Estado { get; private set; }
        public TipoTarea Tipo { get; private set; }
        public Guid IdTareaPadre { get; private set; }
        public List<Tarea> Consecuencias { get; private set; }

        public Tarea(Guid idReferencia, Guid idCreador, Guid idEjecutor, DateTime vigenteDesde, DateTime vencimiento, int alerta, string descripcion, string instrucciones, TipoTarea tipoTarea, Guid idTareaPadre)
        {

            if (vigenteDesde < DateTime.Now) throw new TareaDomainException("La Tarea no puede estar vigente antes del día de su creación");
            if (Vencimiento < vigenteDesde.AddDays(alerta)) throw new TareaDomainException("No puede crear una tarea vencida");
            if (descripcion == string.Empty) throw new TareaDomainException("La descripción de la tarea no puede estar vacia");

            IdReferencia = idReferencia;
            IdCreador = idCreador;
            IdEjecutor = idEjecutor;
            Creacion = DateTime.Now;
            VigenteDesde = vigenteDesde;
            Vencimiento = vencimiento;
            Alerta = alerta;
            Descripcion = descripcion;
            Instrucciones = instrucciones;
            Tipo = tipoTarea;
            IdTareaPadre = idTareaPadre;
        }
        public void DarCumplimiento() { }
        public void Postergar(int Dias) { }
        public void Anular(string Motivo) { }
        public void Pausar(string Motivo) { }
        public void SumarConsecuencia(Tarea tarea, EstadoTarea estado)
        {
            //puede agregar consecuencia 
            if (this.Tipo == TipoTarea.Compleja)
            {

            }
        }
        public void QuitarConsecuencia(Guid idTarea)
        {

        }

        public void Clonar() { } // Crear un tarea idéntica e independiente
        public void Clonar(int Cantidad, int Periodo) { } // Crear varias tareas idénticas e independientes
        public void CrearTareaConsecuente(int Periodo) { } // Crear una tarea idéntica pero dependiente
        public void CrearTareasConsecuente(List<Tarea> Tareas, int Periodo) { } // Crear varias tareas diferentes y dependientes
    }
}