using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent
{
    private event Action onEvent;
    public void OnEvent() => onEvent?.Invoke();
    public void Subscribe(Action action) => onEvent += action;
    public void Unsubscribe(Action action) => onEvent -= action;
    public void Clear() => onEvent = null;

}

public class GameEvent<T>
{
    private event Action<T> onEvent;
    public void OnEvent(T value) => onEvent?.Invoke(value);
    public void Subscribe(Action<T> action) => onEvent += action;
    public void Unsubscribe(Action<T> action) => onEvent -= action;
    public void Clear() => onEvent = null;
}

public class GameEvent<T, T2>
{
    private event Action<T, T2> onEvent;
    public void OnEvent(T value, T2 value2) => onEvent?.Invoke(value, value2);
    public void Subscribe(Action<T, T2> action) => onEvent += action;
    public void Unsubscribe(Action<T, T2> action) => onEvent -= action;
    public void Clear() => onEvent = null;
}
