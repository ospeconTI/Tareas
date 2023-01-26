namespace OSPeConTI.Tareas.Domain.Entities
{
    public class TipoTarea : Enumeration
    {
        public static TipoTarea Simple = new TipoTarea(1, nameof(Simple).ToLowerInvariant());

        public static TipoTarea Compleja = new TipoTarea(2, nameof(Compleja).ToLowerInvariant());

        public TipoTarea(int id, string nombre) : base(id, nombre)
        {
        }

        public static IEnumerable<TipoTarea> List() => new[] { Comun, Generadora };

        public static TipoTarea FromName(string nombre)
        {
            var state = List().SingleOrDefault(s => String.Equals(s.Nombre, nombre, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new Exception($"Valores posibles para TipoTarea: {String.Join(",", List().Select(s => s.Nombre))}");
            }

            return state;
        }

        public static TipoTarea From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new Exception($"Valores posibles para TipoTarea: {String.Join(",", List().Select(s => s.Nombre))}");
            }

            return state;
        }
    }
}
