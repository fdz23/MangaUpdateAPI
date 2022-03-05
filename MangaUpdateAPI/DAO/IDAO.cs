namespace MangaUpdateAPI.DAO
{
    public interface IDAO<T> where T : Model.Model
    {
        List<T> GetAll();

        T GetById(int id);

        int Insert(T instance);

        void Update(T instance);
    }
}
