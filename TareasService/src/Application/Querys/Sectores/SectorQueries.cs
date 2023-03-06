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
                string query =
                @"SELECT c.*,u.*
                    FROM  dbo.Sector c
                    left join Usuario u on c.id = u.SectorId
                    where c.Id = @id;";

                IEnumerable<SectorDTO> retorno = await FormatQuery(connection, query, new { id });


                return retorno.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<SectorDTO>> GetByDescripcionAsync(string descripcion)

        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query =
                @"SELECT c.*,u.*
                    FROM  dbo.Sector c
                    left join Usuario u on c.id = u.SectorId
                    where c.Descripcion like '%' + @descripcion + '%' Order by c.Descripcion";

                IEnumerable<SectorDTO> retorno = await FormatQuery(connection, query, new { descripcion });

                return retorno;

            }
        }

        public async Task<IEnumerable<SectorDTO>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query =
                @"SELECT c.*,u.*
                    FROM     dbo.Sector c
                    left join Usuario u on c.id = u.SectorId
                    Order by c.Descripcion desc";


                IEnumerable<SectorDTO> retorno = await FormatQuery(connection, query, null);

                return retorno;
            }
        }


        public async Task<IEnumerable<SectorDTO>> FormatQuery(SqlConnection connection, string query, object parametros)
        {
            Dictionary<Guid, SectorDTO> sectores = new Dictionary<Guid, SectorDTO>();

            IEnumerable<SectorDTO> retorno = await connection.QueryAsync<SectorDTO, UsuarioDTO, SectorDTO>(query, (s, u) =>
            {
                if (!sectores.TryGetValue(s.Id, out SectorDTO sector))
                {
                    sectores.Add(s.Id, sector = s);
                }
                if (sector.Usuarios == null)
                {
                    sector.Usuarios = new List<UsuarioDTO>();
                }

                if (u != null)
                {
                    UsuarioDTO usuario = new UsuarioDTO();
                    usuario.Id = u.Id;
                    usuario.Apellido = u.Apellido;
                    usuario.Nombre = u.Nombre;
                    usuario.Email = u.Email;

                    sector.Usuarios.Add(usuario);

                }


                return sector;
            }, parametros);



            var tmp = retorno.GroupBy(g => g.Id).Select(d =>
                            {
                                var primero = d.First();

                                return primero;
                            });

            return tmp;
        }






    }

}