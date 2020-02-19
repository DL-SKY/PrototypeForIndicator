/*
= Паттерн Singleton =
*/

using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Variables
    private static T instance;
    #endregion

    #region Properties
    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<T>();

            return Singleton<T>.instance;
        }
    }

    public static bool IsInstantiated
    {
        get
        {
            return (object)Singleton<T>.instance != null;
        }
    }
    #endregion

    #region Unity methods
    private void OnDestroy()
    {
        instance = null;
    }
    #endregion
}

