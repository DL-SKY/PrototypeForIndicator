/*
= Паттерн Singleton =
*/

using UnityEngine;

namespace DllSky.Patterns
{
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
                {
                    instance = GameObject.FindObjectOfType<T>();
                }

                if (instance == null)
                {
                    var obj = new GameObject(string.Format("Singleton_{0}", typeof(T).Name));
                    instance = obj.AddComponent<T>();
                }

                if ((object)instance == null)
                {
                    Debug.LogWarning((object)string.Format("No {0} instance set", (object)typeof(T).FullName));
                }

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
        protected virtual void Awake()
        {
            if (IsInstantiated)
                instance = GetComponent<T>();
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }
        #endregion
    }
}
