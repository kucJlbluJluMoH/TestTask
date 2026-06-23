using System;
public static class EventBus<T> where T : struct, IEvent
{
    public static event Action<T> OnEvent;

    public static void Raise(T evt)
    {
        OnEvent?.Invoke(evt);
    }
}
