using System;
using UnityEngine;

namespace DllSky.Protection
{
    [Serializable]
    public class IntProtect
    {
        [SerializeField]
        private string amount;

#if UNITY_EDITOR
        [SerializeField]
        private int showOnlyEditorValue;
#endif

        public IntProtect()
        {

        }

        public IntProtect(int _value)
        {
            Set(_value);
        }

        public int Get()
        {
            try
            {
                return int.Parse(SimpleEncrypting.Decode(amount));
            }
            catch
            {
                return 0;
            }
        }

        public void Set(int _value)
        {
            amount = SimpleEncrypting.Encode(_value.ToString());

#if UNITY_EDITOR
            showOnlyEditorValue = Get();
#endif
        }

        public static IntProtect operator + (IntProtect a, IntProtect b)
        {
            var A = a.Get();
            var B = b.Get();

            return new IntProtect(A + B);
        }
    }

    [Serializable]
    public class FloatProtect
    {
        [SerializeField]
        private string amount;

#if UNITY_EDITOR
        [SerializeField]
        private float showOnlyEditorValue;
#endif

        public FloatProtect()
        {

        }

        public FloatProtect(float _value)
        {
            Set(_value);
        }

        public float Get()
        {
            try
            {
                return float.Parse(SimpleEncrypting.Decode(amount), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                return 0.0f;
            }
        }

        public void Set(float _value)
        {
            amount = SimpleEncrypting.Encode(_value.ToString(System.Globalization.CultureInfo.InvariantCulture));

#if UNITY_EDITOR
            showOnlyEditorValue = Get();
#endif
        }

        public static FloatProtect operator + (FloatProtect a, FloatProtect b)
        {
            var A = a.Get();
            var B = b.Get();

            return new FloatProtect(A + B);
        }
    }
}
