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
    }
}
