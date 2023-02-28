namespace OSPeConTI.Tareas.Application.Queries
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Linq;

    public class LocalidadQueries
        : ILocalidadQueries
    {
        private string _connectionString = string.Empty;

        public LocalidadQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<LocalidadDTO> GetLocalidadAsync(Guid id)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion, c.ProvinciasId, p.Descripcion as ProvinciaNombre, c.CodigoPostal, c.CodigoSSS
                    FROM     dbo.Localidad c inner join dbo.Provincias p on c.ProvinciasId=p.Id where c.Id = @id;"
                    , new { id });

                var localidad = multiple.Read<LocalidadDTO>().First();

                if (localidad == null)
                    throw new KeyNotFoundException();

                return localidad;
            }
        }

        public async Task<IEnumerable<LocalidadDTO>> GetLocalidadByNameAsync(string descripcion)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion, c.ProvinciasId, p.Descripcion as ProvinciaNombre, c.CodigoPostal, c.CodigoSSS
                    FROM     dbo.Localidad c inner join dbo.Provincias p on c.ProvinciasId=p.Id where c.Descripcion like '%' + @descripcion + '%' Order by c.Descripcion;"
                    , new { descripcion });

                var localidad = multiple.Read<LocalidadDTO>().ToList();

                if (localidad == null)
                    throw new KeyNotFoundException();

                return localidad;
            }
        }

        public async Task<IEnumerable<LocalidadDTO>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                    @"SELECT c.Id, c.Descripcion, c.ProvinciasId, p.Descripcion as ProvinciaNombre, c.CodigoPostal, c.CodigoSSS
                    FROM     dbo.Localidad c inner join dbo.Provincias p on c.ProvinciasId=p.Id Order by c.Descripcion desc;");

                var localidad = multiple.Read<LocalidadDTO>().ToList();

                return localidad;
            }
        }

        public async Task<List<LocalidadDTO>> GetLocalidadByProvincia(Guid provinciaId)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion, c.ProvinciasId, p.Descripcion as ProvinciaNombre, c.CodigoPostal, c.CodigoSSS
                    FROM     dbo.Localidad c inner join dbo.Provincias p on c.ProvinciasId=p.Id where c.Id = @id;"
                    , new { provinciaId });

                var localidad = multiple.Read<LocalidadDTO>().ToList();

                if (localidad == null)
                    throw new KeyNotFoundException();

                return localidad;
            }
        }


    }



}