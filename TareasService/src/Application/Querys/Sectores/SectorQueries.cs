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

            string query =
            @"SELECT c.*,u.*
                    FROM  dbo.Sector c
                    left join Usuario u on c.id = u.SectorId
                    where c.Id = @id;";

            IEnumerable<SectorDTO> retorno = await QueryToObject(query, new { id });

            return retorno.FirstOrDefault();

        }

        public async Task<IEnumerable<SectorDTO>> GetByDescripcionAsync(string descripcion)

        {

            string query =
            @"SELECT c.*,u.*
                    FROM  dbo.Sector c
                    left join Usuario u on c.id = u.SectorId
                    where c.Descripcion like '%' + @descripcion + '%' Order by c.Descripcion";

            return await QueryToObject(query, new { descripcion });


        }

        public async Task<IEnumerable<SectorDTO>> GetAll()
        {
            string query =
            @"SELECT c.*,u.*
                    FROM     dbo.Sector c
                    left join Usuario u on c.id = u.SectorId
                    Order by c.Descripcion desc";

            return await QueryToObject(query, null);

        }


        public async Task<IEnumerable<SectorDTO>> QueryToObject(string query, object parametros)
        {
            IEnumerable<SectorDTO> retorno;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                Dictionary<Guid, SectorDTO> sectores = new Dictionary<Guid, SectorDTO>();

                retorno = await connection.QueryAsync<SectorDTO, UsuarioDTO, SectorDTO>(query, (s, u) =>
                {
                    if (!sectores.TryGetValue(s.Id, out SectorDTO sector))
                    {
                        sectores.Add(s.Id, sector = s);
                        sector.Usuarios = new List<UsuarioDTO>();
                    }

                    if (u.Apellido != null) sector.Usuarios.Add(u);

                    return sector;

                }, parametros);
            }

            return retorno.Distinct().ToList();

        }
    }
}