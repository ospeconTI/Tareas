namespace OSPeConTI.Tareas.Application.Queries
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Linq;

    public class SectorQueries
        : ISectorQueries
    {
        private string _connectionString = string.Empty;

        public SectorQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<SectorDTO> GetAsync(Guid id)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion
                    FROM     dbo.Sector c where c.Id = @id;"
                    , new { id });

                var Sector = multiple.Read<SectorDTO>().First();

                if (Sector == null)
                    throw new KeyNotFoundException();

                return Sector;
            }
        }

        public async Task<IEnumerable<SectorDTO>> GetByDescripcionAsync(string descripcion)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion
                    FROM     dbo.Sector c where c.Descripcion like '%' + @descripcion + '%' Order by c.Descripcion;"
                    , new { descripcion });

                var Sector = multiple.Read<SectorDTO>().ToList();

                if (Sector == null)
                    throw new KeyNotFoundException();

                return Sector;
            }
        }

        public async Task<IEnumerable<SectorDTO>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                    @"SELECT c.Id, c.Descripcion
                    FROM     dbo.Sector c Order by c.Descripcion desc;");

                var Sector = multiple.Read<SectorDTO>().ToList();

                return Sector;
            }
        }


    }

}