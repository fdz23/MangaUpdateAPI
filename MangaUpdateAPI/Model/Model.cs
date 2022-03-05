using System.Data.SqlClient;

namespace MangaUpdateAPI.Model
{
    public abstract class Model
    {
        public int Id { get; set; }

        public abstract void Populate(SqlDataReader reader);
    }
}
