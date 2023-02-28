namespace OSPeConTI.Tareas.Application.Queries
{
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Linq;

    public class TipoDocumentoQueries
        : ITipoDocumentoQueries
    {
        private string _connectionString = string.Empty;

        public TipoDocumentoQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<TipoDocumentoDTO> GetTipoDocumentoAsync(Guid id)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion
                    FROM     dbo.TipoDocumento c where c.Id = @id;"
                    , new { id });

                var tipoDocumento = multiple.Read<TipoDocumentoDTO>().First();

                if (tipoDocumento == null)
                    throw new KeyNotFoundException();

                return tipoDocumento;
            }
        }

        public async Task<IEnumerable<TipoDocumentoDTO>> GetTipoDocumentoByNameAsync(string descripcion)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                   @"SELECT c.Id, c.Descripcion, c.CodigoSSS
                    FROM     dbo.TipoDocumento c where c.Descripcion like '%' + @descripcion + '%' Order by c.Descripcion;"
                    , new { descripcion });

                var tipoDocumento = multiple.Read<TipoDocumentoDTO>().ToList();

                if (tipoDocumento == null)
                    throw new KeyNotFoundException();

                return tipoDocumento;
            }
        }

        public async Task<IEnumerable<TipoDocumentoDTO>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var multiple = await connection.QueryMultipleAsync(
                    @"SELECT c.Id, c.Descripcion, c.CodigoSSS
                    FROM     dbo.TipoDocumento c Order by c.Descripcion desc;");

                var tipoDocumento = multiple.Read<TipoDocumentoDTO>().ToList();

                return tipoDocumento;
            }
        }


    }

}