using UnityEngine;

public interface IGameplayEntity
{
    int Id { get; }
    Transform EntityTransform { get; }
}
