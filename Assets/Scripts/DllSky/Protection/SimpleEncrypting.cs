using System;
using System.Text;

namespace DllSky.Protection
{
    //Base64 преобразование
    public static class SimpleEncrypting
    {
        public static string Revert(this string _str)
        {
            var result = "";

            for (int i = _str.Length-1; i >= 0; i--)
            {
                result += _str[i];
            }

            return result;
        }

        public static string RandomChar(this string _str)
        {
            if (string.IsNullOrEmpty(_str))
                return "a";

            var symbol = _str.ToCharArray(UnityEngine.Random.Range(0, _str.Length), 1);

            return symbol[0].ToString();
        }

        /// <summary>
        /// "Шифрование". Преобразуем в Base64
        /// </summary>
        public static string Encode(string _commonText)
        {
            if (string.IsNullOrEmpty(_commonText))
                return _commonText;

            //Байты
            var plainTextBytes = Encoding.UTF8.GetBytes(_commonText);
            //Строка64
            var simpleEncode = Convert.ToBase64String(plainTextBytes);
            //Обратная строка
            var revertEncode = simpleEncode.Revert();

            //Возвращяем со случайным первым символом
            return revertEncode.Insert(0, revertEncode.RandomChar());
        }

        /// <summary>
        /// "Дешифровка". Преобразуем обратно из Base64
        /// </summary>
        public static string Decode(string _base64Text)
        {
            if (string.IsNullOrEmpty(_base64Text))
                return _base64Text;

            try
            {
                //Шифрованная обратная строка без первого символа
                _base64Text = _base64Text.Remove(0, 1);
                //Прямая зашифрованная строка
                var revertDecode = _base64Text.Revert();

                //Байты обратной зашифрованой строки (убираем первый символ, разворачиваем)
                var base64EncodedBytes = Convert.FromBase64String(revertDecode);
                //Расшифрованная строка
                var simpleDecode = Encoding.UTF8.GetString(base64EncodedBytes);

                return simpleDecode;
            }
            catch
            {
                return "0";
            }        
        }
    }
}
