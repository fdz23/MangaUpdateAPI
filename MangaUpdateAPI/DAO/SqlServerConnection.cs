using System.Data.SqlClient;

namespace MangaUpdateAPI.DAO
{
    public class SqlServerConnection
    {
        private string _connectionString = "Server=DESKTOP-3IDEOE2;Database=MangaUpdate;User Id=ApplicationLogin;Password=@F3rn4nd023;";

        public SqlConnection ObtainConnection()
        {
            var con = new SqlConnection(_connectionString);

            con.Open();

            return con;
        }
    }
}
