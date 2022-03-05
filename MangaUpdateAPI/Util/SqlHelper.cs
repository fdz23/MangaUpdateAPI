namespace MangaUpdateAPI.Util
{
    public static class SqlHelper
    {
        public static string ObtainColumns(Type type)
        {
            return string.Join(",", type.GetProperties().Where(f => f.CustomAttributes.Any()).Select(f => f.Name));
        }

        public static string ObtainValues(object instance)
        {
            var type = instance.GetType();

            return string.Join(",", ObtainCorrectValueForProperties(type.GetProperties().Where(f => f.CustomAttributes.Any()).Select(f => f.GetValue(instance)).ToList()));
        }

        public static string ObtainColumnsIntoValues(object instance)
        {
            var type = instance.GetType();
            var commands = new List<string>();
            var fields = type.GetProperties().Where(f => f.CustomAttributes.Any()).ToList();

            for (var i = 0; i < fields.Count; i++)
            {
                var isString = fields[i].PropertyType == typeof(string);
                var quote = isString ? "'" : "";
                string command = $"{fields[i].Name} = {quote}{fields[i].GetValue(instance)}{quote}";
                commands.Add(command);
            }

            return string.Join(",", commands);
        }

        private static List<string> ObtainCorrectValueForProperties(List<object> values)
        {
            var properValues = new List<string>();

            for (var i = 0; i < values.Count; i++)
            {
                var type = values[i].GetType();
                var isString = type == typeof(string);
                var quote = isString ? "'" : "";
                string command = $"{quote}{values[i]}{quote}";

                properValues.Add(command);
            }

            return properValues;
        }
    }
}