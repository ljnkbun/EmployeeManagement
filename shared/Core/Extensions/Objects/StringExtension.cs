using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Extensions.Objects
{
    public static class StringExtension
    {
        public static string RemoveEndWith(this string source, string value)
        {
            if (!string.IsNullOrEmpty(source) && source.Length > 0 && source.EndsWith(value))
            {
                return source.Remove(source.Length - 1, 1).RemoveEndWith(value);
            }
            return source;
        }
        public static string RemoveStartWith(this string source, string value)
        {
            if (!string.IsNullOrEmpty(source) && source.Length > 0 && source.StartsWith(value))
            {
                return source.Remove(0, 1).RemoveStartWith(value);
            }
            return source;
        }
        public static string FirstCharToLowerCase(this string source)
        {
            if (!string.IsNullOrEmpty(source) && char.IsUpper(source[0]))
                return source.Length == 1 ? char.ToLower(source[0]).ToString() : char.ToLower(source[0]) + source[1..];

            return source;
        }

        public static string ToNonUnicode(this string text)
        {
            string[] arr1 = [ "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                                            "đ",
                                            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                                            "í","ì","ỉ","ĩ","ị",
                                            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                                            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                                            "ý","ỳ","ỷ","ỹ","ỵ",];
            string[] arr2 = [ "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                                            "d",
                                            "e","e","e","e","e","e","e","e","e","e","e",
                                            "i","i","i","i","i",
                                            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                                            "u","u","u","u","u","u","u","u","u","u","u",
                                            "y","y","y","y","y",];
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        public static string ToSafeLower(this string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : text.ToLower();
        }

    }
}
