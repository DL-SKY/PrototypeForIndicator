using System;
using System.Collections;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    #region Variables
    public string ScreenName => screenName;
    protected string screenName;

    protected bool isInit = false;                  //Флаг Инициализации. Нужен, что бы при потере фокуса или активации/деактивации определить был ли Экран активен до этого
    protected bool isOpened = true;
    public bool IsOpened => isOpened;
    #endregion

    #region Properties
    public bool IsInit
    {
        get { return isInit; }
    }
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
