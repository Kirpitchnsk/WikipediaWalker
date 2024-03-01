namespace WikipediaWalkerClassLibrary
{
    /// <summary>
    /// Класс с одной функцией, осуществляющий проверку на возможность преобразования строки в число
    /// </summary>
    public class InputValidator
    {
        /// <summary>
        /// Проверяет возможно ли преобразование строки в число или нет. В случае успеха число будет записано в переменную number
        /// </summary>
        /// <param name="data">Строка, которую нужно преобразовать в число</param>
        /// <param name="number">Целочисленная переменная в которую записывается преобразованное значение в случае успешного преобразования</param>
        /// <returns></returns>
        public static bool CheckNumber(string data,out int number)
        {
            // Число, в которую запишется результат
            var userInputNumber = 0;

            try
            {
                // Преобразование в число
                userInputNumber = int.Parse(data);
            }
            catch (FormatException)
            {
                // Ошибка, преобразуемое не строка
                number = 0;
                return false;
            }
            catch (OverflowException)
            {
                // Ошибка, число вышло за границы допустимости
                number = 0;
                return false;
            }
            // Если преобразование прошло успешно возвращаем число
            number = userInputNumber;
            return true;
        }
    }
}