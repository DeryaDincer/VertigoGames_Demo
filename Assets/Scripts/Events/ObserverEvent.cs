using VertigoGames.Datas.Reward;

namespace VertigoGames.Events
{
    public class ObserverEvent
    {
       
    }

    #region Gameplay Events
    public class WheelSpinStartedEvent : ObserverEvent
    {
       
    }
    
    public class WheelSpinCompletedEvent : ObserverEvent
    {
        public readonly RewardData RewardData;
        public readonly int RewardAmount;

        public WheelSpinCompletedEvent(RewardData rewardData, int rewardAmount)
        {
            RewardData = rewardData;
            RewardAmount = rewardAmount;
        }
    }
    
    public class RewardDeterminedEvent : ObserverEvent
    {
        public readonly ZoneData ZoneData;
        public readonly int CurrentZoneIndex;
        public readonly RewardData RewardData;
        public readonly int RewardAmount;

        public RewardDeterminedEvent(ZoneData zoneData, int currentZoneIndex, RewardData rewardData, int rewardAmount)
        {
            ZoneData = zoneData;
            CurrentZoneIndex = currentZoneIndex;
            RewardData = rewardData;
            RewardAmount = rewardAmount;
        }
    }
    #endregion

    #region UI Events
    public class DeadZoneRewardEvent : ObserverEvent { }

    public class InputBlockStateChangedEvent : ObserverEvent
    {
        public readonly bool IsBlock;

        public InputBlockStateChangedEvent(bool isBlock)
        {
            IsBlock = isBlock;
        }
    }
    
    public class RewardAnimationStartedEvent : ObserverEvent
    {
        public readonly UIRewardAnimationInfo UIRewardAnimationInfo;

        public RewardAnimationStartedEvent(UIRewardAnimationInfo uıRewardAnimationInfo)
        {
            UIRewardAnimationInfo = uıRewardAnimationInfo;
        }
    }
    
    public class WindowStateChangedEvent : ObserverEvent
    {
        public readonly WindowStateChangeInfo WindowStateChangeInfo;

        public WindowStateChangedEvent(WindowStateChangeInfo windowStateChangeInfo)
        {
            WindowStateChangeInfo = windowStateChangeInfo;
        }
    }
    #endregion

    #region Session Events
    public class GameSessionResetEvent : ObserverEvent { }

    public class GameSessionRevivedEvent : ObserverEvent { }
    #endregion

    #region Economy Events
    public class CurrencyChangedEvent : ObserverEvent
    {
        public readonly RewardType RewardType;
        
        public CurrencyChangedEvent(RewardType rewardType)
        {
            RewardType = rewardType;
        }
    }
    #endregion
}