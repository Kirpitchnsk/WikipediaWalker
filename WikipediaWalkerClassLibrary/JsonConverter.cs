using Newtonsoft.Json;

namespace WikipediaWalkerClassLibrary
{
    public class JsonConverter<T>
    {
        public string ConvertToJson(T obj)
        {
            try
            {
                // Преобразование объекта в JSON строку
                string jsonString = JsonConvert.SerializeObject(obj);
                return jsonString;
            }
            catch (Exception ex)
            {
                // Обработка ошибок преобразования
                Console.WriteLine($"Ошибка преобразования в JSON: {ex.Message}");
                return null;
            }
        }

        public T ConvertFromJson(string jsonString)
        {
            try
            {
                // Преобразование JSON строки в объект типа T
                T obj = JsonConvert.DeserializeObject<T>(jsonString);
                return obj;
            }
            catch (Exception ex)
            {
                // Обработка ошибок преобразования
                Console.WriteLine($"Ошибка преобразования из JSON: {ex.Message}");
                return default;
            }
        }
    }
}
