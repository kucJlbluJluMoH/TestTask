using System.Collections.Generic;
using UnityEngine;
public class ActiveEntitiesTracker : MonoBehaviour
{
    private readonly HashSet<IGameplayEntity> _activeEntities = new HashSet<IGameplayEntity>();

    private void OnEnable()
    {
        EventBus<EntityStateChangedEvent>.OnEvent += HandleEntityStateChanged;
    }

    private void OnDisable()
    {
        EventBus<EntityStateChangedEvent>.OnEvent -= HandleEntityStateChanged;
    }

    private void HandleEntityStateChanged(EntityStateChangedEvent evt)
    {
        if (evt.IsActive)
        {
            _activeEntities.Add(evt.Entity);
        }
        else
        {
            _activeEntities.Remove(evt.Entity);
        }
    }

    public IReadOnlyCollection<IGameplayEntity> GetActiveEntities()
    {
        return _activeEntities;
    }
}
