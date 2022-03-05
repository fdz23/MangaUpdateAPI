using MangaUpdateAPI.Util;
using System.Data.SqlClient;

namespace MangaUpdateAPI.Model
{
    public class User : Model
    {

        [Insertable(IsInsertable = true)]
        public string Name { get; set; }

        public List<Manga> Mangas { get; set; }

        public override void Populate(SqlDataReader reader)
        {
            Id = Convert.ToInt32(reader["UserId"].ToString());
            Name = reader["Name"].ToString();
        }
    }
}
