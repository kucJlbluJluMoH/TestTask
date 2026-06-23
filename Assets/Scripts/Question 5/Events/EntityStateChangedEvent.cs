public struct EntityStateChangedEvent : IEvent
{
    public IGameplayEntity Entity;
    public bool IsActive;
}
