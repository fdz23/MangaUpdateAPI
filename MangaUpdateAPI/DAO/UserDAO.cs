using MangaUpdateAPI.Model;
using System.Data;
using System.Data.SqlClient;

namespace MangaUpdateAPI.DAO
{
    public class UserDAO : DAO<User>
    {
        public UserDAO() : base("MangaUser") { }

        public User CreateNewUser(User user, Identity identity)
        {
            int userId = Insert(user);

            string sql = $"INSERT INTO MangaIdentity (MangaUserId,Username,Password) VALUES ({userId},'{identity.Username}','{identity.Password}')";

            using var con = new SqlServerConnection().ObtainConnection();
            var cmd = new SqlCommand(sql, con);

            cmd.ExecuteNonQuery();

            return user;
        }

        public User ValidateCredentials(string username, string password)
        {
            string procedure = "ValidateUser";
            using var con = new SqlServerConnection().ObtainConnection();
            SqlCommand cmd = new SqlCommand(procedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
            cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                var user = new User();
                user.Populate(reader);

                return user;
            }

            return null;
        }
    }
}
