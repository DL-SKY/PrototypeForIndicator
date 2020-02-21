using System.Collections;

public class MainMenuScreenController : ScreenController
{
    #region Variables
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public override void Initialize(object _data)
    {
        base.Initialize(_data);

        StartCoroutine(Initializing());
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    private IEnumerator Initializing()
    {
        //Загружаем сцену
        yield return MainGameManager.Instance.LoadSceneCoroutine(ConstantsScene.FIRST_SCENE);

        StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        yield return SplashScreenManager.Instance.HideSplashScreen();
    }
    #endregion
}
