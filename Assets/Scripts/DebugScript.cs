using System;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    [SerializeField] private Transform _popUpCanvas;
    private PopupService _popupService;
    private void OnEnable()
    {
        _popupService = new PopupService(_popUpCanvas);
    }
    [ContextMenu("Save")]
    public void Save()
    {
        TestData data = new TestData
        {
            IntValue = 67,
            StringValue = "Hello, World!",
            FloatValue = 3.14f,
        };
        SaveSystem.Save("test_data", data);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        TestData data = SaveSystem.Load<TestData>("test_data");
        if (data != null)
        {
            Debug.Log($"Loaded Data: IntValue={data.IntValue}, StringValue={data.StringValue}, FloatValue={data.FloatValue}");
        }
        else
        {
            Debug.Log("No data found.");
        }
    }
    
    [ContextMenu("Show Test Popup")]
    public void ShowTestPopup()
    {
        PopupData data = new PopupData { Title = "Quit?", Body = "Are you sure?" }
            .AddButton("Yes", TestYes)
            .AddButton("No", TestNo);
        _popupService.Show(data);
    }
    
    [ContextMenu("Close Test Popup")]
    public void CloseTestPopup()
    {
        _popupService.Close();
    }
    

    private void TestYes()
    {
        Debug.Log("TestYes");
    }
    
    private void TestNo()
    {
        Debug.Log("TestNo");
    }

}

public class TestData
{
    public int IntValue;
    public string StringValue;
    public float FloatValue;
}