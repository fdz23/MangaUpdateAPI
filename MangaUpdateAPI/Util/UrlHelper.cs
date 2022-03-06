using MangaUpdateAPI.Model;

namespace MangaUpdateAPI.Util
{
    public static class UrlHelper
    {
        public static string GetNextChapterUrl(Manga manga)
        {
            var nextChapter = manga.ObtainNextChapter();

            var split = manga.Url.Split(manga.LastChapterRead);
            if (split.Length == 1)
                return null;

            if (split.Length == 2)
                return String.Join(nextChapter.ToString(), split);

            if (split.Length > 2)
            {
                var lastSplit = split[split.Length - 1];

                var newSplit = split.SkipLast(1);
                var url = String.Join(manga.LastChapterRead, newSplit);

                return url + nextChapter + lastSplit;
            }

            return null;
        }
    }
}
