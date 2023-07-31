using System.Text.RegularExpressions;

namespace Util
{
    public static class StringValidations
    {
        private static readonly Regex _onlyNumberRegex = new Regex(@"^[0-9]+$");

        public static bool HasOnlyNumbers(string text)
        {
            if (_onlyNumberRegex.IsMatch(text)) return true;

            return false;
        }

        public static bool HasOnlyLetters(string text)
        {
            if(text.All(Char.IsLetter)) return true;

            return false;
        }
    }
}