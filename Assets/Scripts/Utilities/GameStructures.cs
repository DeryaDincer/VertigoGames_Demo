using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace VertigoGames.Utility
{
    public class GameStructures
    {

    }
}

public struct WindowStateChangeInfo
{
    public WindowType WindowType;
    public bool ActiveStatus;
    public object CustomData;
    
    public WindowStateChangeInfo(WindowType windowType, bool active, object customData)
    {
        WindowType = windowType;
        ActiveStatus = active;
        CustomData = customData;
    }
}

public class ZoneBarIndicatorInfo
{
    public TextMeshProUGUI Text { get; private set; }
    public int Value { get; set; }

    public ZoneBarIndicatorInfo(TextMeshProUGUI text, int value)
    {
        Text = text;
        Value = value;
    }
}