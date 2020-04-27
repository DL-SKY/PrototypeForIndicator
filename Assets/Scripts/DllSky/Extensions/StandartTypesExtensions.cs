using System;
using UnityEngine;

namespace DllSky.Extensions
{
    public static class StandartTypesExtensions
    {
        public static int ToInt(this float _float)
        {
            return Mathf.RoundToInt(_float);
        }

        //public static Vector2 Rotate(this Vector2 _point, float _angle)
        //{
        //    var radToGrad = (float)Math.PI / 180.0f;

        //    var x = _point.x * (float)Math.Cos(_angle * radToGrad) - _point.y * (float)Math.Sin(_angle * radToGrad);
        //    var y = _point.x * (float)Math.Sin(_angle * radToGrad) + _point.y * (float)Math.Cos(_angle * radToGrad);

        //    return new Vector2(x, y);
        //}

        //public static Vector3 RotateByAxisY(this Vector3 _point, Vector2 _right, Vector2 _up)
        //{
        //    var result =  _point.x * _right + _point.y * _up;
        //    return new Vector3(result.x, _point.y, result.y);
        //}
    }
}
