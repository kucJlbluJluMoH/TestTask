using UnityEngine;
public class GameplayEntity : MonoBehaviour, IGameplayEntity
{
    public int Id
    {
        get
        {
            return gameObject.GetInstanceID();
        }
    }
    public Transform EntityTransform
    {
        get
        {
            return transform;
        }
    }

    private void OnEnable()
    {
        EventBus<EntityStateChangedEvent>.Raise(new EntityStateChangedEvent
        {
            Entity = this,
            IsActive = true,
        });
    }

    private void OnDisable()
    {
        EventBus<EntityStateChangedEvent>.Raise(new EntityStateChangedEvent
        {
            Entity = this,
            IsActive = false,
        });
    }
}
