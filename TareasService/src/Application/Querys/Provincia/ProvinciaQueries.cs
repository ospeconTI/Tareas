namespace OSPeConTI.Tareas.Application.Queries
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Linq;

    public class ProvinciaQueries
        : IProvinciaQueries
    {
        private string _connectionString = string.Empty;

        public ProvinciaQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<ProvinciaDTO> GetProvinciaAsync(Guid id)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion
                    FROM     dbo.Provincia c where c.Id = @id;"
                    , new { id });

                var provincia = multiple.Read<ProvinciaDTO>().First();

                if (provincia == null)
                    throw new KeyNotFoundException();

                return provincia;
            }
        }

        public async Task<IEnumerable<ProvinciaDTO>> GetProvinciaByNameAsync(string descripcion)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion, c.CodigoSSS
                    FROM     dbo.Provincia c where c.Descripcion like '%' + @descripcion + '%' Order by c.Descripcion;"
                    , new { descripcion });

                var provincia = multiple.Read<ProvinciaDTO>().ToList();

                if (provincia == null)
                    throw new KeyNotFoundException();

                return provincia;
            }
        }

        public async Task<IEnumerable<ProvinciaDTO>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                    @"SELECT c.Id, c.Descripcion, c.CodigoSSS
                    FROM     dbo.Provincia c Order by c.Descripcion desc;");

                var provincia = multiple.Read<ProvinciaDTO>().ToList();

                return provincia;
            }
        }


    }

}