namespace OSPeConTI.Tareas.Application.Queries
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Linq;

    public class NacionalidadQueries
        : INacionalidadQueries
    {
        private string _connectionString = string.Empty;

        public NacionalidadQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<NacionalidadDTO> GetNacionalidadAsync(Guid id)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion
                    FROM     dbo.Nacionalidad c where c.Id = @id;"
                    , new { id });

                var nacionalidad = multiple.Read<NacionalidadDTO>().First();

                if (nacionalidad == null)
                    throw new KeyNotFoundException();

                return nacionalidad;
            }
        }

        public async Task<IEnumerable<NacionalidadDTO>> GetNacionalidadByNameAsync(string descripcion)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion, c.CodigoSSS
                    FROM     dbo.Nacionalidad c where c.Descripcion like '%' + @descripcion + '%' Order by c.Descripcion;"
                    , new { descripcion });

                var nacionalidad = multiple.Read<NacionalidadDTO>().ToList();

                if (nacionalidad == null)
                    throw new KeyNotFoundException();

                return nacionalidad;
            }
        }

        public async Task<IEnumerable<NacionalidadDTO>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                    @"SELECT c.Id, c.Descripcion, c.CodigoSSS
                    FROM     dbo.Nacionalidad c Order by c.Descripcion desc;");

                var nacionalidad = multiple.Read<NacionalidadDTO>().ToList();

                return nacionalidad;
            }
        }


    }

}