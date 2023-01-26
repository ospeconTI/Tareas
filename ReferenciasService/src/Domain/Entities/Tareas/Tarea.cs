using System;
using System.Collections.Generic;
using System.Linq;


namespace OSPeConTI.Tareas.Domain.Entities
{
    public class Tarea : Entity, IAggregateRoot
    {
        public Tarea()
        {


        }
        public int IdReferencia { get; private set; }
        public Sector IdCreador { get; private set; }
        public Sector IdEjecutor { get; private set; }
        public DateTime Creacion { get; private set; }
        public DateTime Vencimiento { get; private set; }
        public int Alerta { get; private set; }
        public string Descripcion { get; private set; }
        public string Instrucciones { get; private set; }
        public List<string> Adjuntos { get; private set; }
        public EstadoTarea Estado { get; private set; }
        public TipoTarea Tipo { get; private set; }
        public Guid IdTareaPadre { get; private set; }
        public List<Consecuencia> Consecuencias { get; private set; }

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