/*
© Alexander Danilovsky, 2017
//------------------------------------------------
= Скрипт различных утилит и функций =

 * public static void OnVibrate(int _count, float _pause, bool _bVibrateEnable)
 * public static string GetMD5(string _string)
 * public static Color ColorStringHexToColorRGB(string _hex)
 * public static string GetStringFormatAmount3(int _amount)
 * public static string GetStringFormatAmount5(int _amount)
 * public static string GetStringFormatAmount6(int _amount)
 * ...
*/

using DllSky.Extensions;
using System.Collections;
using System.Security.Cryptography;							//MD5
using System.Text;                                          //Encoding
using UnityEngine;
using UnityEngine.Networking;

namespace DllSky.Utility
{
    public static class UtilityBase
    {
        //------------------------------------------------
        //Вызов вибрации
        /// <summary>
        /// Сопрограмма вибрации.
        /// _count - кол-во/продолжительность, _pause - пауза между вибрацией, _bVibrateEnable - настройка в конфиге.
        /// </summary>	
        public static IEnumerator VibrateCoroutine(int _count, float _pause = 0.7f, bool _bVibrateEnable = true)
        {
            if (!_bVibrateEnable)
                yield break;

            //#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
#if UNITY_ANDROID
            for (int i = 0; i < _count; i++)
            {
                Handheld.Vibrate();
                yield return new WaitForSeconds(_pause);
            }            
#endif
        }
        //------------------------------------------------
        //MD5 текста
        /// <summary>
        /// Вычисляет md5. Кириллица корректна для UTF8.
        /// </summary>
        public static string GetMD5(string _string)
        {
            string result = "";
            byte[] hash = Encoding.UTF8.GetBytes(_string);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);

            foreach (var b in hashenc)
                result += b.ToString("x2");

            return result;
        }
        //------------------------------------------------
        //String to Color
        /// <summary>
        /// Конвертирует строку в цвет (БЕЗ АЛЬФА-КАНАЛА!)
        /// </summary>
        /// <param name="_hex"></param>
        /// <returns></returns>
        public static Color ColorStringHexToColorRGB(string _hex)
        {
            byte r = byte.Parse(_hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(_hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(_hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte a = 255;
            return new Color32(r, g, b, a);
        }
        //------------------------------------------------
        //Formating amount
        /// <summary>
        /// Конвертирует количество в 1К
        /// </summary>
        /// <param name="_amount"></param>
        /// <returns></returns>
        public static string GetStringFormatAmount3(int _amount)
        {
            string result = ToStringFormatAmount(_amount, 3);
            return result;
        }

        /// <summary>
        /// Конвертирует количество в 100К
        /// </summary>
        /// <param name="_amount"></param>
        /// <returns></returns>
        public static string GetStringFormatAmount5(int _amount)
        {
            string result = ToStringFormatAmount(_amount, 5);
            return result;
        }

        /// <summary>
        /// Конвертирует количество в 1КК
        /// </summary>
        /// <param name="_amount"></param>
        /// <returns></returns>
        public static string GetStringFormatAmount6(int _amount)
        {
            string result = ToStringFormatAmount(_amount, 6);
            return result;
        }

        private static string ToStringFormatAmount(int _amount, int _format)
        {
            float divider = Mathf.Pow(10,_format);
            float amount = _amount;
            string result = "";

            if (amount >= divider)
            {
                do
                {
                    result += "K";
                    amount /= 1000.0f;
                }
                while (amount >= divider);

                result = string.Format("{0:F1}{1}", amount, result);
            }
            else
                result = string.Format("{0:F0}", amount);

            return result;
        }
        //------------------------------------------------
        /// <summary>
        /// Предлагает отправить письмо
        /// </summary>
        /// <param name="_address"></param>
        /// <param name="_subject"></param>
        /// <param name="_body"></param>
        /// <param name="_escapeURL"></param>
        public static void SendToEmail(string _address, string _subject, string _body, bool _escapeURL = true)
        {
            Debug.Log("<color=#FFD800>[SEND EMAIL] </color>" + _address + " / \"" + _subject + "\"");

            if (_escapeURL)
            {
                _subject = UnityWebRequest.EscapeURL(_subject);
                _body = UnityWebRequest.EscapeURL(_body);
            }

            string url = string.Format("mailto:{0}?subject={1}&body={2}", _address, _subject, _body);

            Application.OpenURL(url);            
        }
    }

    ////https://stackoverrun.com/ru/q/11374570
    //public static class ResourcesManager
    //{
    //    //------------------------------------------------
    //    //Загрузка префабов
    //    public static GameObject LoadPrefab(string _path, string _name)
    //    {
    //        return Resources.Load<GameObject>(_path + _name);
    //    }
    //    //------------------------------------------------
    //    public static T Load<T>(string _path, string _name) where T : Object
    //    {
    //        return Resources.Load<T>(_path + _name);
    //    }
    //    //------------------------------------------------
    //}
}
