using DllSky.Managers;
using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Example Main Menu Screen component + Example using event system
/// </summary>
public class MainMenuScreenController : ScreenController
{
    #region Variables
    [Space()]
#pragma warning disable 0649
    [SerializeField]
    private TextMeshProUGUI moneyLabel; 
    [SerializeField]
    private int money;
    #endregion

    #region Unity methods
    private void Start()
    {
        UpdateMoneyUI();
    }

    private void OnEnable()
    {
        //Example using event system
        //Подписываемся на событие при активации Экрана : Когда откуда-либо будет вызвано событие "ExampleEvent" - будет выполнен метод "OnExampleHandler"
        EventManager.AddEventListener("ExampleEvent", OnExampleHandler);
    }

    private void OnDisable()
    {
        //Example using event system
        //Отписываемся от события при деактивации Экрана (Не забываем отписываться!)
        EventManager.RemoveEventListener("ExampleEvent", OnExampleHandler);
    }
    #endregion

    #region Public methods
    public override void Initialize(object _data)
    {
        base.Initialize(_data);

        StartCoroutine(Initializing());
    }

    /// <summary>
    /// Example using event system. Пример вызова события
    /// </summary>
    public void OnClickExampleEventButton()
    {
        //Вызываем событие "ExampleEvent" с аргументом "10". 
        EventManager.DispatchEvent(new CustomEvent("ExampleEvent", 10));
    }

    /// <summary>
    /// Example using Dialog
    /// </summary>
    public void OnClickExampleDialog()
    {
        ScreenManager.Instance.ShowDialog<ExampleDialog>(ConstantsDialog.EXAMPLE);
    }
    #endregion

    #region Protected methods
    #endregion

    #region Private methods
    private void UpdateMoneyUI()
    {
        moneyLabel.text = string.Format("MONEY: {0}", money);
    }

    /// <summary>
    /// Example using event system. Пример с передачей аргументов 
    /// </summary>
    /// <param name="_event"></param>
    private void OnExampleHandler(CustomEvent _event)
    {
        //EventData может быть любым типом, главное - явное преобразование типов
        //в данном примере мы ожидаем, что EventData - целое число (н-р, количество полученных монет)
        //если событие не нуждается в аргументах - просто не используем EventData
        var arg = (int)_event.EventData;

        money += arg;
        UpdateMoneyUI();
    }
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
