using System;
using System.Collections.Generic;
using OSPeConTI.Tareas.Domain.SeedWork;
using System.Linq;

namespace OSPeConTI.Tareas.Domain.Entities
{
    public class EstadoTarea : Enumeration
    {
        public static EstadoTarea Cumplida = new EstadoTarea(1, nameof(Cumplida).ToLowerInvariant());

        public static EstadoTarea Vencida = new EstadoTarea(2, nameof(Vencida).ToLowerInvariant());

        public static EstadoTarea Alerta = new EstadoTarea(3, nameof(Alerta).ToLowerInvariant());

        public static EstadoTarea Anulada = new EstadoTarea(4, nameof(Anulada).ToLowerInvariant());

        public static EstadoTarea Pausada = new EstadoTarea(5, nameof(Pausada).ToLowerInvariant());

        public static EstadoTarea Pendiente = new EstadoTarea(6, nameof(Pendiente).ToLowerInvariant());

        public EstadoTarea(int id, string nombre) : base(id, nombre)
        {
        }

        public static IEnumerable<EstadoTarea> List() => new[] { Cumplida, Vencida, Alerta, Anulada, Pausada, Pendiente };


        public static EstadoTarea FromName(string nombre)
        {
            var state = List().SingleOrDefault(s => String.Equals(s.Nombre, nombre, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new Exception($"Valores posibles para EstadoTarea: {String.Join(",", List().Select(s => s.Nombre))}");
            }

            return state;
        }

        public static EstadoTarea From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new Exception($"Valores posibles para EstadoTarea: {String.Join(",", List().Select(s => s.Nombre))}");
            }

            return state;
        }
    }
};

