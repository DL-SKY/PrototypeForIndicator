using System;
using System.Collections;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    #region Variables
    public string DialogName => dialogName;
    protected string dialogName;

    public bool canCloseWithEsc = true;
    public bool result = true;

    [Header("Animation")]
    public bool withAnimation = false;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected GameObject bg;
    [SerializeField]
    protected GameObject content;

    protected Action<bool> Callback;                    //Коллбек для вызова при закрытии Диалога

    protected bool isInit = false;                      //Флаг Инициализации. Нужен, что бы при потере фокуса или активации/деактивации определить был ли Диалог активен до этого
    protected bool isOpened = true;
    public bool IsOpened => isOpened;
    #endregion

    #region Unity methods
    protected virtual void OnAwake()
    {
        if (withAnimation && !animator)
            animator = GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        //EventManager.eventOnClickEsc += OnEsc;

        if (withAnimation && animator)
            animator.Play("Show");     //Запускается анимация, которая содержит вызов метода InitBeforeShowContent() и InitAfterAnimation()
    }

    protected virtual void OnDisable()
    {
        //EventManager.eventOnClickEsc -= OnEsc;
    }
    #endregion

    #region Public methods
    public void SetName(string _name)
    {
        dialogName = _name;
    }

    public void SetCallback(Action<bool> _callbackCloseDialog)
    {
        Callback = _callbackCloseDialog;
    }

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
            if (animator)
                animator.Play("Hide");    //Анимация должна вызывать метод CloseDialogImmediately()
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
        if (animator)
            animator.Play("Hide");
        else
            CloseSplashScreenImmediately();
    }

    public void CloseSplashScreenImmediately()
    {
        result = true;
        isOpened = false;       

        Destroy(gameObject);
    }

    //Метод, который должен вызываться в анимации исчезания Диалога
    public virtual void CloseDialogImmediately()
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
            Close(false);
    }
    #endregion

    #region Coroutine
    [Obsolete("Using: while (IsOpened) yield return null;")]
    public IEnumerator WaitShowSplashScreen()
    {
        while (!isInit)
            yield return null;
    }

    [Obsolete("Using: while (IsOpened) yield return null;")]
    public IEnumerator Wait()
    {
        while (isOpened)
            yield return null;
    }
    #endregion
}
