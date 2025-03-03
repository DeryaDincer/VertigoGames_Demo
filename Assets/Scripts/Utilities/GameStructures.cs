using System.Collections.Generic;
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