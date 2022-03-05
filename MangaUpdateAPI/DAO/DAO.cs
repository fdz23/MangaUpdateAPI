using MangaUpdateAPI.Util;
using System.Data.SqlClient;

namespace MangaUpdateAPI.DAO
{
    public class DAO<T> : IDAO<T> where T : Model.Model, new()
    {
        protected string _tableName;
        protected string _primaryKey;

        protected DAO(string tableName)
        {
            _tableName = tableName;
            _primaryKey = $"{typeof(T).Name}Id";
        }

        public virtual List<T> GetAll()
        {
            string sql = $"SELECT * FROM {_tableName}";
            var rows = new List<T>();

            using (var con = new SqlServerConnection().ObtainConnection())
            {
                var cmd = new SqlCommand(sql, con);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    T item = new();
                    item.Populate(reader);

                    rows.Add(item);
                }
            }

            return rows;
        }

        public virtual T GetById(int id)
        {
            string sql = $"SELECT * FROM {_tableName} WHERE {_primaryKey} = {id}";

            using (var con = new SqlServerConnection().ObtainConnection())
            {
                var cmd = new SqlCommand(sql, con);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    T item = new();
                    item.Populate(reader);

                    return item;
                }
            }

            return null;
        }

        public virtual int Insert(T instance)
        {
            string columns = SqlHelper.ObtainColumns(typeof(T));
            string values = SqlHelper.ObtainValues(instance);
            string sql = $"INSERT INTO {_tableName} ({columns}) OUTPUT Inserted.{_primaryKey} VALUES ({values})";

            using var con = new SqlServerConnection().ObtainConnection();
            var cmd = new SqlCommand(sql, con);
            var result = cmd.ExecuteScalar();
            return (int)result;
        }

        public virtual void Update(T instance)
        {
            string columnsIntoValues = SqlHelper.ObtainColumnsIntoValues(instance);
            string sql = $"UPDATE {_tableName} SET ({columnsIntoValues}) WHERE {_primaryKey} = {instance.Id}";

            using var con = new SqlServerConnection().ObtainConnection();
            var cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
        }
    }
}
