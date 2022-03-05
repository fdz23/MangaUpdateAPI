using MangaUpdateAPI.Util;
using System.Data.SqlClient;

namespace MangaUpdateAPI.Model
{
    public class Identity : Model
    {
        public User User { get; set; }

        [Insertable(IsInsertable = true)]
        public string Username { get; set; }


        [Insertable(IsInsertable = true)]
        public string Password { get; set; }

        public override void Populate(SqlDataReader reader)
        {
            Id = Convert.ToInt32(reader["MangaIdentityId"].ToString());
            Username = reader["Username"].ToString();
            Password = reader["Password"].ToString();
        }
    }
}
