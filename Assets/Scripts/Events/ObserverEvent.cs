using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VertigoGames.Events
{
    public class ObserverEvent
    {
  
    }

    public class SpinWheelEvent : ObserverEvent
    {
        public SpinWheelEvent() { }
    }
    
    public class OnZoneStateReadyEvent : ObserverEvent
    {
        public ZoneData ZoneData;

        public OnZoneStateReadyEvent(ZoneData zoneData)
        {
            ZoneData = zoneData;
        }
    }
    
    public class OnWheelSpinCompletedEvent : ObserverEvent
    {
        public OnWheelSpinCompletedEvent() { }
    }
    
    public class OnUpdateZoneDataEvent : ObserverEvent
    {
        public ZoneType ZoneType;
        public int CurrentZoneIndex;

        public OnUpdateZoneDataEvent(ZoneType zoneType, int currentZoneIndex)
        {
            ZoneType = zoneType;
            CurrentZoneIndex = currentZoneIndex;
        }
    }
    
    public class InputBlockerEvent : ObserverEvent
    {
        public bool IsBlock { get; private set; }

        public InputBlockerEvent(bool isBlock)
        {
            IsBlock = isBlock;
        }
    }
}