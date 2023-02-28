namespace OSPeConTI.Tareas.Application.Queries
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Linq;

    public class EstadoCivilQueries
        : IEstadoCivilQueries
    {
        private string _connectionString = string.Empty;

        public EstadoCivilQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<EstadoCivilDTO> GetEstadoCivilAsync(Guid id)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion
                    FROM     dbo.EstadoCivil c where c.Id = @id;"
                    , new { id });

                var estadoCivil = multiple.Read<EstadoCivilDTO>().First();

                if (estadoCivil == null)
                    throw new KeyNotFoundException();

                return estadoCivil;
            }
        }

        public async Task<IEnumerable<EstadoCivilDTO>> GetEstadoCivilByNameAsync(string descripcion)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion, c.CodigoSSS
                    FROM     dbo.EstadoCivil c where c.Descripcion like '%' + @descripcion + '%' Order by c.Descripcion;"
                    , new { descripcion });

                var estadoCivil = multiple.Read<EstadoCivilDTO>().ToList();

                if (estadoCivil == null)
                    throw new KeyNotFoundException();

                return estadoCivil;
            }
        }

        public async Task<IEnumerable<EstadoCivilDTO>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                    @"SELECT c.Id, c.Descripcion, c.CodigoSSS
                    FROM     dbo.EstadoCivil c Order by c.Descripcion desc;");

                var estadoCivil = multiple.Read<EstadoCivilDTO>().ToList();

                return estadoCivil;
            }
        }


    }

}