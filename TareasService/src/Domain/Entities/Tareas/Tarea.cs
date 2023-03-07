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
        public int Item { get; private set; }
        public int Cantidad { get; private set; }
        public int Lapso { get; private set; }
        public bool EsPorLapso
        {
            get
            {
                return Lapso != 0;
            }
        }
        public List<Tarea> Consecuencias { get; private set; }

        public Tarea() { }

        private static Tarea Crear(TipoTarea tipo, Guid referenciaId, Sector creador, Sector ejecutor, DateTime vigenteDesde, int venceEn, int alerta, string descripcion, string instrucciones, TipoTarea tipoTarea, List<Tarea> consecuencias)
        {
            if (vigenteDesde < DateTime.Now) throw new TareaDomainException("La Tarea no puede estar vigente antes del día de su creación");
            if (descripcion == string.Empty) throw new TareaDomainException("La descripción de la tarea no puede estar vacia");

            Tarea tarea = new Tarea();
            tarea.Tipo = tipo;

            tarea.ReferenciaId = referenciaId;
            tarea.Creador = creador;
            tarea.Ejecutor = ejecutor;
            tarea.Creacion = DateTime.Now;
            tarea.VigenteDesde = vigenteDesde;
            tarea.Vencimiento = vigenteDesde.AddDays(venceEn);
            tarea.Alerta = alerta;
            tarea.Descripcion = descripcion;
            tarea.Instrucciones = instrucciones;
            tarea.Estado = EstadoTarea.Pendiente;
            tarea.Consecuencias = consecuencias;

            return tarea;

        }

        public static Tarea CrearSimple(Guid referenciaId, Sector creador, Sector ejecutor, DateTime vigenteDesde, int venceEn, int alerta, string descripcion, string instrucciones, TipoTarea tipoTarea)
        {
            return Crear(TipoTarea.Simple, referenciaId, creador, ejecutor, vigenteDesde, venceEn, alerta, descripcion, instrucciones, tipoTarea, null);
        }

        public static Tarea CrearCompleja(Guid referenciaId, Sector creador, Sector ejecutor, DateTime vigenteDesde, int venceEn, int alerta, string descripcion, string instrucciones, TipoTarea tipoTarea, List<Tarea> consecuencias)
        {
            return Crear(TipoTarea.Compleja, referenciaId, creador, ejecutor, vigenteDesde, venceEn, alerta, descripcion, instrucciones, tipoTarea, consecuencias);
        }

        public static Tarea CrearMultiplesPorLapso(int cantidad, int lapsoEnDias, Guid referenciaId, Sector creador, Sector ejecutor, DateTime vigenteDesde, int venceEn, int alerta, string descripcion, string instrucciones, TipoTarea tipoTarea, List<Tarea> consecuencias = null)
        {

            Tarea tarea;
            if (consecuencias == null)
            {
                tarea = Tarea.CrearSimple(referenciaId, creador, ejecutor, vigenteDesde, venceEn, alerta, descripcion, instrucciones, tipoTarea);

            }
            else
            {
                tarea = Tarea.CrearCompleja(referenciaId, creador, ejecutor, vigenteDesde, venceEn, alerta, descripcion, instrucciones, tipoTarea, consecuencias);

            }
            tarea.Lapso = lapsoEnDias;
            tarea.Cantidad = cantidad;
            tarea.Item = tarea.Item + 1;

            return tarea;

        }

        public static List<Tarea> CrearMultiplesEnFecha(int cantidad, int diaDelMes, Guid referenciaId, Sector creador, Sector ejecutor, DateTime vigenteDesde, int venceEn, int alerta, string descripcion, string instrucciones, TipoTarea tipoTarea, List<Tarea> consecuencias = null)
        {

            List<Tarea> tareas = new List<Tarea>();

            int anio = DateTime.Now.Year;
            int mes = DateTime.Now.Month;
            for (var x = 0; x < cantidad; x = x + 1)
            {
                mes = mes + cantidad;
                if (mes > 12)
                {
                    mes = 1;
                    anio = anio + 1;
                };
                vigenteDesde = new DateTime(diaDelMes, mes, anio);
                if (consecuencias == null)
                {

                    Tarea.CrearSimple(referenciaId, creador, ejecutor, vigenteDesde, venceEn, alerta, descripcion, instrucciones, tipoTarea);
                }
                else
                {
                    Tarea.CrearCompleja(referenciaId, creador, ejecutor, vigenteDesde, venceEn, alerta, descripcion, instrucciones, tipoTarea, consecuencias);
                }

            }

            return tareas;

        }
        private static Tarea CrearSiguiente(Tarea tarea)
        {
            return Tarea.CrearMultiplesPorLapso(tarea.Cantidad, tarea.Lapso, tarea.ReferenciaId, tarea.Creador, tarea.Ejecutor, DateTime.Now, (tarea.VigenteDesde - tarea.Vencimiento).Days, tarea.Alerta, tarea.Descripcion, tarea.Instrucciones, tarea.Tipo, tarea.Consecuencias);
        }


        public Tarea DarVencimiento()
        {
            Estado = EstadoTarea.Vencida;

            Tarea tareaSiguiente = null;

            if (EsPorLapso && Cantidad > Item) tareaSiguiente = CrearSiguiente(this);

            return tareaSiguiente;
        }
        public Tarea DarCumplimiento()
        {
            Estado = EstadoTarea.Cumplida;

            Tarea tareaSiguiente = null;

            if (EsPorLapso && Cantidad > Item) tareaSiguiente = CrearSiguiente(this);

            return tareaSiguiente;
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