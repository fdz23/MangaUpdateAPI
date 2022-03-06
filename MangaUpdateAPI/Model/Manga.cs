using MangaUpdateAPI.Util;
using System.Data.SqlClient;

namespace MangaUpdateAPI.Model
{
    public class Manga : Model
    {

        [Insertable(IsInsertable = true)]
        public string Name { get; set; }


        [Insertable(IsInsertable = true)]
        public string Chapter { get; set; }


        [Insertable(IsInsertable = true)]
        public string LastChapterRead { get; set; }


        [Insertable(IsInsertable = true)]
        public string Url { get; set; }

        public bool HasReadLatestChapter() => Equals(Chapter, LastChapterRead);

        public string ObtainNextChapter() => (Convert.ToInt32(LastChapterRead) + 1).ToString();

        public override void Populate(SqlDataReader reader)
        {
            Id = Convert.ToInt32(reader["MangaId"].ToString());
            Name = reader["Name"].ToString();
            Chapter = reader["Chapter"].ToString();
            LastChapterRead = reader["LastChapterRead"].ToString();
            Url = reader["Url"].ToString();
        }
    }
}
