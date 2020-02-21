using DllSky.Patterns;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : Singleton<MainGameManager>
{
    #region Variables
    private string currentScene = null;
    #endregion

    #region Unity methods
    private void Start()
    {
        StartCoroutine(StartGame());
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void OnApplicationPause(bool _pause)
    {
        if (_pause)         //ПАУЗА
        {
            //Сохранение
        }
        else                //ВОССТАНОВЛЕНИЕ
        {

        }
    }

    private void OnApplicationQuit()
    {
        //Сохранение
    }
    #endregion

    #region Public methods
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    private IEnumerator StartGame()
    {
        //Стартовый прелоадер
        yield return SplashScreenManager.Instance.ShowStartingGame();

        //Версия
        Debug.Log("<color=#FFD800>[APP VERSION] " + Application.version + "</color>");       

        //TODO: убрать искусственную паузу
        yield return new WaitForSeconds(1.0f);

        //Загружаем экран World, который загрузит Сцену
        ScreenManager.Instance.ShowScreen(ConstantsScreen.MAIN_MENU);
        //yield return SplashScreenManager.Instance.HideSplashScreen();     //Загрузочные экраны лучше убирать из кода Экранов (в данном случае в MainMenuScreenController)
    }

    public IEnumerator LoadSceneCoroutine(string _scene, LoadSceneMode _mode = LoadSceneMode.Additive)
    {
        //Выгружаем предыдущую сцену
        if (SceneManager.sceneCount > 1)
        {
            var oldScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            var oldName = oldScene.name;

            var unloading = SceneManager.UnloadSceneAsync(oldScene);
            while (!unloading.isDone)
                yield return null;            

            Debug.Log("<color=#FFD800>[MainGameManager] Scene unloaded: " + oldName + "</color>");
        }

        Resources.UnloadUnusedAssets();
        GC.Collect();

        //Загружаем новую сцену
        currentScene = _scene;

        var loading = SceneManager.LoadSceneAsync(currentScene, _mode);
        while (!loading.isDone)
            yield return null;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));
        Debug.Log("<color=#FFD800>[MainGameManager] Scene loaded: " + currentScene + "</color>");
    }
    #endregion
}
