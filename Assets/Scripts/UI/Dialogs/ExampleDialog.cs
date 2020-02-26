using DllSky.Managers;

public class ExampleDialog : DialogController
{
    #region Variables
    #endregion

    #region Unity methods
    #endregion

    #region Public methods
    public override void CloseDialogImmediately()
    {
        base.CloseDialogImmediately();

        //Вызываем событие "ExampleEvent" с аргументом "5". 
        EventManager.DispatchEvent(new CustomEvent("ExampleEvent", 5));
    }
    #endregion

    #region Protected methods
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
