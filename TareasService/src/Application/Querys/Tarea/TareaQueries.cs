namespace OSPeConTI.Tareas.Application.Queries
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Linq;

    public class TareaQueries
        : ITareaQueries
    {
        private string _connectionString = string.Empty;

        public TareaQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<TareaDTO> GetAsync(Guid id)

        {

            string query =
            @"SELECT *
                    FROM  dbo.Tarea t
                    left join Tarea tt on t.id = tt.TareaId
                    where t.Id = @id;";

            IEnumerable<TareaDTO> retorno = await QueryToObject(query, new { id });

            return retorno.FirstOrDefault();

        }

        public async Task<IEnumerable<TareaDTO>> GetByIdReferenciaAsync(Guid id)

        {

            string query =
            @"SELECT *
                    FROM  dbo.Tarea t
                    left join Tarea tt on t.id = tt.TareaId
                    where t.ReferenciaId = @id;";

            return await QueryToObject(query, new { id });


        }

        public async Task<IEnumerable<TareaDTO>> QueryToObject(string query, object parametros)
        {
            IEnumerable<TareaDTO> retorno;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                Dictionary<Guid, TareaDTO> tareas = new Dictionary<Guid, TareaDTO>();

                retorno = await connection.QueryAsync<TareaDTO, TareaDTO, TareaDTO>(query, (t, tt) =>
                {
                    if (!tareas.TryGetValue(t.Id, out TareaDTO tarea))
                    {
                        tareas.Add(t.Id, tarea = t);
                        tarea.Consecuencias = new List<TareaDTO>();
                    }

                    if (tt.Id != null) tarea.Consecuencias.Add(tt);

                    return tarea;

                }, parametros);
            }

            return retorno.Distinct().ToList();

        }
    }
}