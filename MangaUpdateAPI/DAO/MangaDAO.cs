using MangaUpdateAPI.Model;
using System.Data;
using System.Data.SqlClient;

namespace MangaUpdateAPI.DAO
{
    public class MangaDAO : DAO<Manga>
    {
        public MangaDAO() : base("Manga") { }

        public int CreateMangaForUser(Manga manga, User user)
        {
            int mangaId = Insert(manga);

            string sql = $"INSERT INTO MangaFromUser (MangaUserId,MangaId) OUTPUT Inserted.{_primaryKey} VALUES ({user.Id},{mangaId})";

            using var con = new SqlServerConnection().ObtainConnection();
            var cmd = new SqlCommand(sql, con);

            return (int)cmd.ExecuteScalar();
        }

        public List<Manga> GetMangaFromUser(User user)
        {
            var mangas = new List<Manga>();

            string procedure = "ObtainMangaFromUser";
            using var con = new SqlServerConnection().ObtainConnection();
            SqlCommand cmd = new SqlCommand(procedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = user.Id;
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Manga manga = new Manga();
                manga.Populate(reader);

                mangas.Add(manga);
            }

            return mangas;
        }
    }
}
