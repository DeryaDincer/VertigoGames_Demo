using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VertigoGames.Datas.Reward;

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
    public object CustomInfo;
    
    public WindowStateChangeInfo(WindowType windowType, bool active, object customInfo)
    {
        WindowType = windowType;
        ActiveStatus = active;
        CustomInfo = customInfo;
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

public class RewardWindowCustomInfo
{
    public RewardData RewardData;
    public int RewardAmount;

    public RewardWindowCustomInfo(RewardData rewardData, int rewardAmount)
    {
        RewardData = rewardData;
        RewardAmount = rewardAmount;
    }
}

public class DangerZoneWindowCustomInfo
{
    public RewardData RewardData;
    public int RewardAmount;

    public DangerZoneWindowCustomInfo(RewardData rewardData, int rewardAmount)
    {
        RewardData = rewardData;
        RewardAmount = rewardAmount;
    }
}


public class UIRewardAnimationInfo
{
    public RewardData RewardData;
    public int RewardAmount;
    public Vector3 StartAnimationTransform;
    public Vector3 EndAnimationTransform;
    public Action AnimationCompleteAction;

    public UIRewardAnimationInfo(RewardData rewardData, int rewardAmount, Vector3 startAnimationTransform, 
        Vector3 endAnimationTransform, Action animationCompleteAction)
    {
        RewardData = rewardData;
        RewardAmount = rewardAmount;
        StartAnimationTransform = startAnimationTransform;
        EndAnimationTransform = endAnimationTransform;
        AnimationCompleteAction = animationCompleteAction;
    }
}



[System.Serializable]
public struct WheelZoneAppearanceInfo
{
    public ZoneType ZoneType;
    public Sprite WheelBaseSprite;
    public Sprite WheelIndicatorSprite;
}


[System.Serializable]
public struct ZoneBarAppearanceInfo
{
    public ZoneType ZoneType;
    public Sprite ZoneBaseSprite;
}