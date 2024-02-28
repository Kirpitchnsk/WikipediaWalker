namespace WikipediaWalkerClassLibrary
{
    public class InputValidator
    {
        public static bool CheckNumber(string data,out int number)
        {
            int userInputNumber = 0;
            try
            {
                userInputNumber = int.Parse(data);
            }
            catch (FormatException)
            {
                number = 0;
                return false;
            }
            catch (OverflowException)
            {
                number = 0;
                return false;
            }
            number = userInputNumber;
            return true;
        }
    }
}