using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoGames.Datas.Reward;

namespace VertigoGames.Events
{
    public class ObserverEvent
    {
  
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
    
    public class OnDeadZoneReward : ObserverEvent
    {
        public RewardData RewardData;

        public OnDeadZoneReward(RewardData rewardData)
        {
            RewardData = rewardData;
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
    
    public class OnRewardAnimationEvent : ObserverEvent
    {
        public UIRewardAnimationInfo UIRewardAnimationInfo;

        public OnRewardAnimationEvent(UIRewardAnimationInfo uıRewardAnimationInfo)
        {
            UIRewardAnimationInfo = uıRewardAnimationInfo;
        }
    }
    
    public class GameSessionResetEvent : ObserverEvent
    {
    }

    public class GameSessionReviveEvent : ObserverEvent
    {
        
    }
    
    public class OnCurrencyChangedEvent : ObserverEvent
    {
        public RewardType RewardType;
        
        public OnCurrencyChangedEvent(RewardType rewardType)
        {
            RewardType = rewardType;
        }
    }
    
    public class WindowStateChangeEvent : ObserverEvent
    {
        public WindowStateChangeInfo WindowStateChangeInfo { get; private set; }

        public WindowStateChangeEvent(WindowStateChangeInfo windowStateChangeInfo)
        {
            WindowStateChangeInfo = windowStateChangeInfo;
        }
    }
}