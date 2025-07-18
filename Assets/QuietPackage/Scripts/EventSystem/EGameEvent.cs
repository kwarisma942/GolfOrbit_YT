using System;
using System.Collections.Generic;

public class EGameEvent
{
    private static readonly Dictionary<EEvents, Action<EData>> eventsDict;

    static EGameEvent()
    {
        if (eventsDict == null)
            eventsDict = new Dictionary<EEvents, Action<EData>>();
    }

    public static void AddListener(EEvents _eventName, Action<EData> _listener)
    {
        if (eventsDict.TryGetValue(_eventName, out Action<EData> _event))
        {
            _event += _listener;
            eventsDict[_eventName] = _event;
        }
        else
        {
            _event += _listener;
            eventsDict.Add(_eventName, _event);
        }
    }

    public static void RemoveListener(EEvents _eventName, Action<EData> _listener)
    {
        if (eventsDict.TryGetValue(_eventName, out Action<EData> _event))
        {
            _event -= _listener;
            eventsDict[_eventName] = _event;
        }
    }

    //public static void Invoke(EEvents _eventName, EData data)
    //{
    //    if (eventsDict.TryGetValue(_eventName, out Action<EData> _event))
    //    {
    //        Debug.Log($"[EventManager] Invoking event: {_eventName} with data: {data}");
    //        _event.Invoke(data);
    //    }
    //    else
    //    {
    //        Debug.LogWarning($"[EventManager] No subscribers found for event: {_eventName}");
    //    }
    //}

    public static void Invoke(EEvents _eventName, EData data)
    {
        if (eventsDict.TryGetValue(_eventName, out Action<EData> _event))
            _event.Invoke(data);
    }

}

public class EData : EventArgs
{
    private object[] args;

    public EData(params object[] _args)
    {
        args = _args;
    }

    public T Get<T>(int index)
    {
        return (T)args[index];
    }
}