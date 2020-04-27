using System.Collections.Generic;
using UnityEngine;

namespace DllSky.Extensions
{
    public static class TransformExtensions
    {
        public static void DestroyChildren(this Transform _transform)
        {
            for (int i = _transform.childCount-1; i >= 0; i--)
                Object.Destroy(_transform.GetChild(i).gameObject);
        }

        public static List<Transform> GetAllChildren(this Transform _transform)
        {
            var result = new List<Transform>();

            for (int i = 0; i < _transform.childCount; i++)
                result.Add(_transform.GetChild(i));

            return result;
        }
    }
}
