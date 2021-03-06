﻿using DllSky.Patterns;
using System.Collections;
using UnityEngine;

public class SplashScreenManager : Singleton<SplashScreenManager>
{
    #region Variables
    public Transform parent;

    private GameObject splashScreen;    
    #endregion

    #region Unity methods
    protected override void Awake()
    {
        base.Awake();

        if (!parent)
            parent = transform;
    }
    #endregion

    #region Public methods
    public void HideSplashScreenImmediately()
    {
        DialogController controller = splashScreen.GetComponent<DialogController>();
        controller.CloseSplashScreenImmediately();
    }

    public bool IsOpenSplashscreen()
    {
        return splashScreen != null;
    }

    public GameObject GetSplashScreen()
    {
        return splashScreen;
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    public IEnumerator ShowStartingGame()
    {
        if (splashScreen)
            Destroy(splashScreen);

        splashScreen = Instantiate(Resources.Load<GameObject>(string.Format("{0}/{1}", ConstantsUiPath.SPLASHSCREEN, "StartingGame")), parent);
        splashScreen.transform.SetAsLastSibling();

        yield return null;
    }

    //public IEnumerator ShowBlack()
    //{
    //    if (splashScreen)
    //        Destroy(splashScreen);

    //    //splashScreen = Instantiate(ResourcesManager.LoadPrefab(ConstantsResourcesPath.SPLASHSCREEN, "ScreenBlack"), parent);
    //    splashScreen = Instantiate(Resources.Load<GameObject>(string.Format("{0}{1}", ConstantsResourcesPath.SCREENS, "ScreenBlack")), parent);
    //    splashScreen.transform.SetAsLastSibling();

    //    yield return splashScreen.GetComponent<DialogController>().WaitShowSplashScreen();

    //    //if (MainGameManager.Instance.needAutoSaving)
    //    //{
    //    //    EventManager.CallOnSave();
    //    //    MainGameManager.Instance.needAutoSaving = false;
    //    //}
    //}

    public IEnumerator HideSplashScreen(bool _withAnimation = true)
    {
        if (!splashScreen)
            yield break;

        DialogController controller = splashScreen.GetComponent<DialogController>();

        if (_withAnimation)
            controller.CloseSplashScreen();
        else
            controller.CloseSplashScreenImmediately();

        //Obsolete
        //yield return controller.WaitShowSplashScreen();
        while (controller.IsOpened)
            yield return null;
    }

    /*public IEnumerator HideSplashScreenImmediately()
    {
        DialogController controller = splashScreen.GetComponent<DialogController>();

        controller.CloseSplashScreenImmediately();
        yield return controller.Wait();
    }*/
    #endregion
}
