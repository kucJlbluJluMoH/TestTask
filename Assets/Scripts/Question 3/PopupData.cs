using System;
using System.Collections.Generic;

public class PopupData
{
    public string Title;
    public string Body;
    public List<ButtonConfig> Buttons = new List<ButtonConfig>();

    public PopupData AddButton(string label, Action onClick)
    {
        if (Buttons.Count >= 5)
        {
            UnityEngine.Debug.LogWarning("[PopupData] Max 5 buttons allowed.");
            return this;
        }

        Buttons.Add(new ButtonConfig { Label = label, OnClick = onClick });
        return this;
    }
}

public class ButtonConfig
{
    public string Label;
    public Action OnClick;
}
