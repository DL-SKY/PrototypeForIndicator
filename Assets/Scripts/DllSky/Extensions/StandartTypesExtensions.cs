using UnityEngine;

namespace DllSky.Extensions
{
    public static class StandartTypesExtensions
    {
        public static int ToInt(this float _float)
        {
            return Mathf.RoundToInt(_float);
        }
    }
}
