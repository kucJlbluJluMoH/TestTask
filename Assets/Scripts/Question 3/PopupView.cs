using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _body;
    [SerializeField] private Transform _buttonContainer;

    private readonly List<GameObject> _spawnedButtons = new List<GameObject>();

    public void Setup(PopupData data, GameObject buttonPrefab)
    {
        _title.text = data.Title;
        _body.text = data.Body;

        ClearButtons();

        foreach (ButtonConfig config in data.Buttons)
        {
            GameObject go = Instantiate(buttonPrefab, _buttonContainer);
            Button btn = go.GetComponent<Button>();
            TMP_Text label = go.GetComponentInChildren<TMP_Text>();

            if (label != null) label.text = config.Label;
            if (btn != null) btn.onClick.AddListener(() => config.OnClick?.Invoke());

            _spawnedButtons.Add(go);
        }
    }

    public void Close()
    {
        ClearButtons();
        Destroy(gameObject);
    }

    private void ClearButtons()
    {
        foreach (GameObject btn in _spawnedButtons)
            Destroy(btn);

        _spawnedButtons.Clear();
    }
}
