namespace OSPeConTI.Tareas.Application.Queries
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Linq;

    public class ParentescoQueries
        : IParentescoQueries
    {
        private string _connectionString = string.Empty;

        public ParentescoQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<ParentescoDTO> GetParentescoAsync(Guid id)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion
                    FROM     dbo.Parentesco c where c.Id = @id;"
                    , new { id });

                var parentesco = multiple.Read<ParentescoDTO>().First();

                if (parentesco == null)
                    throw new KeyNotFoundException();

                return parentesco;
            }
        }

        public async Task<IEnumerable<ParentescoDTO>> GetParentescoByNameAsync(string descripcion)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion, c.CodigoSSS
                    FROM     dbo.Parentesco c where c.Descripcion like '%' + @descripcion + '%' Order by c.Descripcion;"
                    , new { descripcion });

                var parentesco = multiple.Read<ParentescoDTO>().ToList();

                if (parentesco == null)
                    throw new KeyNotFoundException();

                return parentesco;
            }
        }

        public async Task<IEnumerable<ParentescoDTO>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                    @"SELECT c.Id, c.Descripcion, c.CodigoSSS
                    FROM     dbo.Parentesco c Order by c.Descripcion desc;");

                var parentesco = multiple.Read<ParentescoDTO>().ToList();

                return parentesco;
            }
        }


    }

}