using System;
using System.Collections;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    #region Variables
    public string ScreenName => screenName;
    protected string screenName;

    public bool IsInit => isInit;
    protected bool isInit = false;                  //Флаг Инициализации. Нужен, что бы при потере фокуса или активации/деактивации определить был ли Экран активен до этого
    public bool IsOpened => isOpened;
    protected bool isOpened = true;    
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public void SetName(string _name)
    {
        screenName = _name;
    }

    public virtual void Initialize(object _data)
    {
        isInit = true;
    }

    public virtual void Close()
    {
        isOpened = false;

        ScreenManager.Instance.CloseScreen(this);

        Destroy(gameObject);
    }
    #endregion

    #region Protected methods
    #endregion

    #region Coroutine
    [Obsolete("Using: while (IsOpened) yield return null;")]
    public IEnumerator Wait()
    {
        while (isOpened)
            yield return null;
    }
    #endregion
}
