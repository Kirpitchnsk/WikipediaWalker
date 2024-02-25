using Newtonsoft.Json;

namespace WikipediaWalkerClassLibrary
{
    public class JsonConverter<T>
    {
        /// <summary>
        /// Преобразование объекта в формат json
        /// </summary>
        /// <param name="obj">Объект необходимый для преобразования</param>
        /// <returns>Строка в формате json</returns>
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

        /// <summary>
        /// Преобразование строки из json формата в объект
        /// </summary>
        /// <param name="jsonString">Строка в json</param>
        /// <returns>Возвращает объект класса</returns>
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
