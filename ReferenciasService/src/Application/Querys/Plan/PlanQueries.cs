namespace OSPeConTI.ReferenciasService.Application.Queries
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Linq;

    public class PlanQueries
        : IPlanQueries
    {
        private string _connectionString = string.Empty;

        public PlanQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<PlanDTO> GetPlanAsync(Guid id)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion
                    FROM     dbo.Plan c where c.Id = @id;"
                    , new { id });

                var plan = multiple.Read<PlanDTO>().First();

                if (plan == null)
                    throw new KeyNotFoundException();

                return plan;
            }
        }

        public async Task<IEnumerable<PlanDTO>> GetPlanByNameAsync(string descripcion)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion, c.CodigoSSS
                    FROM     dbo.Plan c where c.Descripcion like '%' + @descripcion + '%' Order by c.Descripcion;"
                    , new { descripcion });

                var plan = multiple.Read<PlanDTO>().ToList();

                if (plan == null)
                    throw new KeyNotFoundException();

                return plan;
            }
        }

        public async Task<IEnumerable<PlanDTO>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                    @"SELECT c.Id, c.Descripcion, c.CodigoSSS
                    FROM     dbo.Plan c Order by c.Descripcion desc;");

                var plan = multiple.Read<PlanDTO>().ToList();

                return plan;
            }
        }


    }

}