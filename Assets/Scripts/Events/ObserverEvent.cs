using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoGames.Datas.Reward;

namespace VertigoGames.Events
{
    public class ObserverEvent
    {
  
    }

    // public class SpinWheelEvent : ObserverEvent
    // {
    //     public SpinWheelEvent() { }
    // }
    
    public class OnRewardDeterminedEvent : ObserverEvent
    {
        public OnRewardDeterminedEvent() { }
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
        public RewardData RewardData;
        public int RewardAmount;

        public OnWheelSpinCompletedEvent(RewardData rewardData, int rewardAmount)
        {
            RewardData = rewardData;
            RewardAmount = rewardAmount;
        }
    }
    
    // public class OnUpdateZoneDataEvent : ObserverEvent
    // {
    //     public ZoneData ZoneData;
    //     public int CurrentZoneIndex;
    //
    //     public OnUpdateZoneDataEvent(ZoneData zoneData, int currentZoneIndex)
    //     {
    //         ZoneData = zoneData;
    //         CurrentZoneIndex = currentZoneIndex;
    //     }
    // }
    
    public class OnRewardDetermined : ObserverEvent
    {
        public ZoneData ZoneData;
        public int CurrentZoneIndex;
        public RewardData RewardData;
        public int RewardAmount;

        public OnRewardDetermined(ZoneData zoneData, int currentZoneIndex, RewardData rewardData, int rewardAmount)
        {
            ZoneData = zoneData;
            CurrentZoneIndex = currentZoneIndex;
            RewardData = rewardData;
            RewardAmount = rewardAmount;
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
    
    public class WindowActivateDataEvent : ObserverEvent
    {
        public WindowActivateData WindowActivateData { get; private set; }

        public WindowActivateDataEvent(WindowActivateData windowActivateData)
        {
            WindowActivateData = windowActivateData;
        }
    }
}