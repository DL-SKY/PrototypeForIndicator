using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : Singleton<ScreenManager>
{
    #region Variables
    public Transform parentScreens;
    public Transform parentDialogs;

    [SerializeField]
    private List<ScreenController> screens = new List<ScreenController>();
    [SerializeField]
    private List<DialogController> dialogs = new List<DialogController>();
    #endregion

    #region Unity methods
    private void Start()
    {
        screens.Clear();
        dialogs.Clear();
    }

    private void Update()
    {
        //Кнопка "Назад"
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("<color=#FFD800>[INFO]</color> [ScreenManager] " + KeyCode.Escape);

            //EventManager.CallOnClickEsc();
        }
    }
    #endregion

    #region Public methods
    public void ShowScreen(string _name, object _data = null)
    {
        //Проверка с текущим экраном
        if (screens.Count > 0 && screens[screens.Count - 1].screenName == _name)
        {
            Debug.LogWarning("[ScreenManager] Trying to re-open the screen: " + _name);
            SplashScreenManager.Instance.HideSplashScreenImmediately();
            return;
        }

        ScreenController screen = null;

        //Проверка со список открытых экранов
        foreach (var item in screens)
        {
            if (item.screenName == _name)
            {
                Debug.LogWarning("[ScreenManager] The screen \"" + _name + "\" is in the history of open screens.");

                screen = item;
                screen.gameObject.SetActive(true);

                //...
                //return;
            }
        }

        if (screen == null)
            screen = Instantiate(Resources.Load<GameObject>(string.Format("{0}{1}", ConstantsUiPath.SCREEN, _name)), parentScreens).GetComponent<ScreenController>();

        screen.transform.SetAsLastSibling();
        screen.Initialize(_data);
        screen.screenName = _name;

        //Деактивируем предыдущие экраны
        foreach (var item in screens)
        {
            var itemGO = item.gameObject;

            if (itemGO.activeSelf && item != screen)
                itemGO.SetActive(false);
        }

        screens.Add(screen);
        Debug.Log("<color=#FFD800>[ScreenManager] Screen loaded: " + _name + "</color>");
    }

    public void CloseScreen(ScreenController _screen)
    {
        screens.Remove(_screen);

        screens[screens.Count - 1].transform.SetAsLastSibling();
        screens[screens.Count - 1].gameObject.SetActive(true);
    }

    public T ShowDialog<T>(string _name) where T : DialogController
    {
        var dialog = Instantiate(Resources.Load<GameObject>(string.Format("{0}{1}", ConstantsUiPath.DIALOG, _name)), parentDialogs).GetComponent<T>();
        dialog.transform.SetAsLastSibling();
        dialog.dialogName = _name;

        dialogs.Add(dialog);
        Debug.Log("<color=#FFD800>[ScreenManager] Dialog loaded: " + _name + "</color>");

        return dialog;
    }

    public DialogController ShowDialog(string _name)
    {
        return ShowDialog<DialogController>(_name);
    }

    public void CloseDialog(DialogController _dialog)
    {
        dialogs.Remove(_dialog);
        Debug.Log("<color=#FFD800>[ScreenManager]</color> Dialog closed: " + _dialog.dialogName);
        
        //Destroy(_dialog.gameObject);
        /*int index = dialogs.FindIndex(x => x.id == _dialog.id);

        Debug.Log(_dialog.id);
        Debug.Log(index);

        if (index > 0)
            dialogs.RemoveAt(index);*/
    }

    public bool CheckLastDialog(DialogController _dialog)
    {
        var result = false;

        if (dialogs[dialogs.Count - 1] == _dialog)
            result = true;

        return result;
    }

    public int GetDialogsCount()
    {
        return dialogs.Count;
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
