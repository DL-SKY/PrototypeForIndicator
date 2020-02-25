//using DllSky.Managers;
using System;
using System.Collections;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    #region Variables
    public string dialogName = "";
    public bool canCloseWithEsc = true;
    public bool result = true;

    [Header("Animation")]
    public bool withAnimation = true;
    public GameObject bg;
    public GameObject content;

    public Action<bool> Callback;

    protected bool isInit = false;
    
    private bool isOpened = true;
    public bool IsOpened => isOpened;
    #endregion

    #region Unity methods
    protected virtual void OnEnable()
    {
        //EventManager.eventOnClickEsc += OnEsc;

        if (withAnimation)
        {
            var dialogAnimator = GetComponent<Animator>();
            if (dialogAnimator != null)
                dialogAnimator.Play("Show");    //Запускается анимация, которая содержит вызов метода InitBeforeShowContent() и InitAfterAnimation()
        }
    }

    protected virtual void OnDisable()
    {
        //EventManager.eventOnClickEsc -= OnEsc;
    }
    #endregion

    #region Public methods
    //Метод, который вызывается в анимации появления перед стартом появления "контента"
    public virtual void InitBeforeShowContent()
    {

    }

    //Метод, который должен вызываться в анимации появления Диалога
    public virtual void InitAfterAnimation()
    {

    }

    public virtual void Close(bool _result)
    {
        result = _result;

        if (withAnimation)
        {
            var dialogAnimator = GetComponent<Animator>();
            if (dialogAnimator != null)
                dialogAnimator.Play("Hide");    //Анимация должна вызывать метод CloseDialogImmediately()
            else
                CloseDialogImmediately();
        }
        else
        {
            CloseDialogImmediately();
        }        
    }

    public void InitSplashScreen()
    {
        isInit = true;
    }

    public void CloseSplashScreen()
    {
        GetComponent<Animator>().Play("Hide");
    }

    public void CloseSplashScreenImmediately()
    {
        result = true;
        isOpened = false;       

        Destroy(gameObject);
    }

    //Метод, который должен вызываться в анимации исчезания Диалога
    public void CloseDialogImmediately()
    {
        isOpened = false;

        ScreenManager.Instance.CloseDialog(this);
        Callback?.Invoke(result);

        Destroy(gameObject);
    }
    #endregion

    #region Protected methods
    protected virtual void OnEsc()
    {
        if (canCloseWithEsc && ScreenManager.Instance.CheckLastDialog(this))
        {
            Close(false);
        }
    }
    #endregion

    #region Coroutine
    public IEnumerator WaitShowSplashScreen()
    {
        while (!isInit)
            yield return null;
    }

    public IEnumerator Wait()
    {
        while (isOpened)
            yield return null;
    }
    #endregion
}
