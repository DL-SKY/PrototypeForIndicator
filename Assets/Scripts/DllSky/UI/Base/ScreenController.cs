using System.Collections;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    #region Variables
    public string ScreenName => screenName;
    protected string screenName;

    protected bool isInit = false;
    protected bool isOpened = true;
    #endregion

    #region Properties
    public bool IsInit
    {
        get { return isInit; }
        set { isInit = value; }
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
    public IEnumerator Wait()
    {
        while (isOpened)
            yield return null;
    }
    #endregion
}
