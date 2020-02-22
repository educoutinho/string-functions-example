using System;

namespace Enginesoft.StringFunctionsSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var list = new System.Collections.Generic.List<string>();
            list.Add("abc!@#$%¨&*,.<>}{[]()123 cão!");
            list.Add("Você come purê e açaí? Vai de metrô fazendo crochê? Pasa no camelô. Âmbar é cor? Câncer de estômago é doença?");

            foreach (var input in list)
            {
                Console.WriteLine(string.Format("- Input:\r\n{0}\r\n", input));
                Console.WriteLine(string.Format("- RemoveDiatrics:\r\n{0}\r\n", RemoveDiacritics(input)));
                Console.WriteLine(string.Format("- RemoveSpecialCharacters:\r\n{0}\r\n", RemoveSpecialCharacters(input, removeSpaces: false, removeDiacritics: false)));
                Console.WriteLine(string.Format("- RemoveAll:\r\n{0}\r\n", RemoveSpecialCharacters(input, removeSpaces: true, removeDiacritics: true)));
                Console.WriteLine("----------------");
                Console.WriteLine();
            }

            Console.WriteLine("bye!");
            Console.ReadKey();
        }

        public static string RemoveSpecialCharacters(string text, bool removeSpaces = false, bool removeDiacritics = true, bool removePageBreak = false, bool trim = false, string exceptions = "")
        {
            string ret = text;

            if (string.IsNullOrEmpty(ret))
                return ret;

            if (removeDiacritics)
                ret = RemoveDiacritics(ret);

            ret = System.Text.RegularExpressions.Regex.Replace(ret, @"\t", " ");

            if (!removeSpaces)
                ret = System.Text.RegularExpressions.Regex.Replace(ret, string.Format(@"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ\s{0}]+?", exceptions), string.Empty);
            else
                ret = System.Text.RegularExpressions.Regex.Replace(ret, string.Format(@"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ{0}]+?", exceptions), string.Empty);

            if (removePageBreak)
                RemovePageBreak(ret);

            if (trim)
                ret = ret.Trim();

            // "a - b" = "a  b", isso remove os espaços seguidos
            ret = ret.Replace("  ", " ");

            return ret;
        }

        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] > 255)
                    sb.Append(text[i]);
                else
                    sb.Append(s_Diacritics[text[i]]);
            }

            return sb.ToString();
        }

        public static string GetNumericCharacters(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            string ret = System.Text.RegularExpressions.Regex.Replace(text, @"[^0-9]+?", string.Empty);

            return ret != null ? ret.Trim() : ret;
        }

        public static string RemoveNonVisibleCharacters(string text)
        {
            if (text == null)
                return null;

            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] >= 32 && text[i] <= 255 && text[i] != 127 && text[i] != 129 && text[i] != 141 && text[i] != 143 && text[i] != 144 && text[i] != 157)
                    sb.Append(text[i]);
            }

            return sb.ToString();
        }

        public static string RemovePageBreak(string text, string separator = " ")
        {
            if (text == null)
                return null;

            text = text.Replace("\r\n", separator);
            text = text.Replace("\r", string.Empty);
            text = text.Replace("\n", separator);
            return text;
        }

        public static string RemoveSpaces(string text)
        {
            if (text == null)
                return null;

            text = text.Replace(" ", string.Empty);
            return text;
        }

        public static string RemoveHtmlTags(string text)
        {
            if (text == null)
                return null;

            text = System.Text.RegularExpressions.Regex.Replace(text, @"<[^>]*>", String.Empty);
            return text;
        }

        public static string MaxLength(string text, int maxLength, string sufixToIncludeWhenTruncated = null)
        {
            if (text == null)
                return null;

            text = text.Trim();

            if (string.IsNullOrEmpty(sufixToIncludeWhenTruncated))
            {
                if (text.Length > maxLength)
                    text = text.Substring(0, maxLength);
            }
            else
            {
                if (text.Length > maxLength)
                    text = text.Substring(0, maxLength - sufixToIncludeWhenTruncated.Length) + sufixToIncludeWhenTruncated;
            }

            return text;
        }

        public static string FixedLength(string text, int length)
        {
            if (text == null)
                return null;

            if (text.Length > length)
                text = text.Substring(0, length);
            else if (text.Length < length)
                text = string.Concat(text, new string(' ', length - text.Length));

            return text;
        }

        private static readonly char[] s_Diacritics = GetDiacritics();

        private static char[] GetDiacritics()
        {
            char[] accents = new char[256];

            for (int i = 0; i < 256; i++)
                accents[i] = (char)i;

            accents[(byte)'á'] = accents[(byte)'à'] = accents[(byte)'ã'] = accents[(byte)'â'] = accents[(byte)'ä'] = 'a';
            accents[(byte)'Á'] = accents[(byte)'À'] = accents[(byte)'Ã'] = accents[(byte)'Â'] = accents[(byte)'Ä'] = 'A';

            accents[(byte)'é'] = accents[(byte)'è'] = accents[(byte)'ê'] = accents[(byte)'ë'] = 'e';
            accents[(byte)'É'] = accents[(byte)'È'] = accents[(byte)'Ê'] = accents[(byte)'Ë'] = 'E';

            accents[(byte)'í'] = accents[(byte)'ì'] = accents[(byte)'î'] = accents[(byte)'ï'] = 'i';
            accents[(byte)'Í'] = accents[(byte)'Ì'] = accents[(byte)'Î'] = accents[(byte)'Ï'] = 'I';

            accents[(byte)'ó'] = accents[(byte)'ò'] = accents[(byte)'ô'] = accents[(byte)'õ'] = accents[(byte)'ö'] = 'o';
            accents[(byte)'Ó'] = accents[(byte)'Ò'] = accents[(byte)'Ô'] = accents[(byte)'Õ'] = accents[(byte)'Ö'] = 'O';

            accents[(byte)'ú'] = accents[(byte)'ù'] = accents[(byte)'û'] = accents[(byte)'ü'] = 'u';
            accents[(byte)'Ú'] = accents[(byte)'Ù'] = accents[(byte)'Û'] = accents[(byte)'Ü'] = 'U';

            accents[(byte)'ç'] = 'c';
            accents[(byte)'Ç'] = 'C';

            accents[(byte)'ñ'] = 'n';
            accents[(byte)'Ñ'] = 'N';

            accents[(byte)'ÿ'] = accents[(byte)'ý'] = 'y';
            accents[(byte)'Ý'] = 'Y';

            return accents;
        }
    }
}
