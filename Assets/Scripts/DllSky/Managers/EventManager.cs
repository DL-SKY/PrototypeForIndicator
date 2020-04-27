using System.Collections.Generic;

namespace DllSky.Managers
{
    #region Delegates
    public delegate void CustomEventHandler(CustomEvent _event);
    #endregion

    public class CustomEvent
    {
        public string EventType
        {
            get;
            private set;
        }

        public object EventData
        {
            get;
            private set;
        }

        public CustomEvent(string _type, object _data = null)
        {
            EventType = _type;
            EventData = _data;
        }
    }

    public class CustomEventWrapper
    {
        public event CustomEventHandler OnHandler;

        public void Invoke(CustomEvent _event)
        {
            OnHandler?.Invoke(_event);
        }

        public void Clear()
        {
            OnHandler = null;
        }
    }

    public static class EventManager
    {
        #region Variables
        private static Dictionary<string, CustomEventWrapper> events = new Dictionary<string, CustomEventWrapper>();
        #endregion

        #region Public methods
        public static void AddEventListener(string _eventType, CustomEventHandler _listener)
        {
            CustomEventWrapper eWrapper = null;

            if (!events.TryGetValue(_eventType, out eWrapper))
            {
                eWrapper = new CustomEventWrapper();
                eWrapper.OnHandler += _listener;
                events.Add(_eventType, eWrapper);
            }
            else
            {
                eWrapper.OnHandler += _listener;
            }
        }

        public static void RemoveEventListener(string _eventType, CustomEventHandler _listener)
        {
            CustomEventWrapper eWrapper = null;

            if (events.TryGetValue(_eventType, out eWrapper))
            {
                eWrapper.OnHandler -= _listener;
            }
        }

        public static void DispatchEvent(CustomEvent _event)
        {
            CustomEventWrapper eWrapper = null;

            if (events.TryGetValue(_event.EventType, out eWrapper))
            {
                eWrapper.Invoke(_event);
            }
        }

        public static void DispatchEvent(string _type, object _data = null)
        {
            var customEvent = new CustomEvent(_type, _data);
            DispatchEvent(customEvent);
        }

        public static void Clear()
        {
            foreach (var _event in events)
                _event.Value.Clear();

            events.Clear();
        }
        #endregion
    }
}
