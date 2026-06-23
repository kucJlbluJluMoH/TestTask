using System;
using System.Collections.Generic;
using UnityEngine;

public class FixedCharactersView : MonoBehaviour
{
    [SerializeField] private List<Character> _characters = new();
    [SerializeField] private float _updateInterval = 0.5f;

    public event Action<int, float> OnCharactersDataChanged;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < _updateInterval)
            return;
        _timer = 0f;
        CalculateAndNotify();
    }

    private void CalculateAndNotify()
    {
        if (_characters == null || _characters.Count == 0)
        {
            OnCharactersDataChanged?.Invoke(0, 0f);
            return;
        }

        float totalValue = 0f;
        int count = 0;

        for (int i = 0; i < _characters.Count; i++)
        {
            if (_characters[i] != null)
            {
                totalValue += _characters[i].Value;
                count++;
            }
        }

        float avgValue = count > 0 ? totalValue / count : 0f;

        OnCharactersDataChanged?.Invoke(count, avgValue); //todo: add an UI manager to display this data in the UI
        #if UNITY_EDITOR
                Debug.Log($"Characters Count: {count}, Average Value: {avgValue}");
        #endif
    }
}

public class Character : MonoBehaviour
{
    public float Value { get; private set; }

    public void SetValue(float newValue)
    {
        Value = newValue;
    }
}
