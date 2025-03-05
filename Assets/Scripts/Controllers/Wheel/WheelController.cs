using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Controllers.Reward;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.GameTasks;
using VertigoGames.Interfaces;
using VertigoGames.Managers;
using VertigoGames.Pooling;
using VertigoGames.Services;
using VertigoGames.Settings;
using VertigoGames.UI.Item.Wheel;
using Random = UnityEngine.Random;

namespace VertigoGames.Controllers.Wheel
{
    public class WheelController : MonoBehaviour, IRegisterable
    {
        [SerializeField] private RectTransform wheelContainer;
        [SerializeField] private Image spinWheelImage;
        [SerializeField] private Image indicatorImage;
        [SerializeField] private RectTransform wheelItemContainer;
        [SerializeField] private RectTransform wheelItemRoot;
        [SerializeField] private WheelSettings wheelSettings;

        private (RewardData reward, int amount) _selectedReward;
        private WheelItemController _wheelItemController;
        private WheelAnimationController _animationController;
        private WheelVisualController _visualController;
        private RewardSelectController _rewardSelectController;
        private ZoneData _currentZoneData;
        private int _currentZoneIndex;
        private ITaskService _taskService;
        
        public void Initialize(ObjectPoolManager poolManager, ITaskService taskService)
        {
            _taskService = taskService;

            _wheelItemController = new WheelItemController(poolManager, wheelSettings, wheelContainer);
            _animationController = new WheelAnimationController(wheelContainer, indicatorImage.rectTransform, wheelItemRoot,wheelSettings);
            _visualController = new WheelVisualController(spinWheelImage, indicatorImage);
            _rewardSelectController = new RewardSelectController();
        }

        #region Registration and Unregistration
        public void Register()
        {
            ObserverManager.Register<WheelSpinStartedEvent>(HandleWheelSpinStarted);
            ObserverManager.Register<RewardDeterminedEvent>(HandleRewardDetermined);
            ObserverManager.Register<DeadZoneRewardEvent>(HandleDeadZoneReward);
        }

        public void Unregister()
        {
            ObserverManager.Unregister<WheelSpinStartedEvent>(HandleWheelSpinStarted);
            ObserverManager.Unregister<RewardDeterminedEvent>(HandleRewardDetermined);
            ObserverManager.Unregister<DeadZoneRewardEvent>(HandleDeadZoneReward);
        }
        #endregion

        public void BeginGameSession(ZoneData zoneData)
        {
            InitializeWheelSession(zoneData);
        }
        
        private void InitializeWheelSession(ZoneData zoneData)
        {
            _wheelItemController.ClearWheelItems();
            _animationController.ResetWheel();
            _currentZoneData = zoneData;
            _currentZoneIndex = 0;

            List<RewardData> selectedRewards =
                _rewardSelectController.SelectRewards(zoneData, wheelSettings.WheelSlotCountValue);

            StartCoroutine(_wheelItemController.SpawnWheelItemsWithAnimation(selectedRewards, _currentZoneIndex,
                InitializeWheelCompleted));
            
            WheelZoneAppearanceInfo appearanceInfo = wheelSettings.GetWheelZoneAppearanceByZoneType(zoneData.ZoneType);
            _visualController.SetWheelVisual(appearanceInfo);
        } 
        
        private void InitializeWheelCompleted()
        {
            ObserverManager.Notify(new InputBlockStateChangedEvent(false));
            _taskService.CompleteTask(TaskType.InitializeWheel);
        }
        
        private void HandleWheelSpinStarted(WheelSpinStartedEvent obj)
        {
            ObserverManager.Notify(new InputBlockStateChangedEvent(true));
            int targetIndex = GetRandomRewardIndex();
            
            RewardData reward = _wheelItemController.GetWheelItem(targetIndex).RewardData;
            _selectedReward = (reward, _wheelItemController.GetWheelItem(targetIndex).RewardAmount);
            _animationController.AnimateSpin(targetIndex, OnWheelSpinComplete);
        }
        
        private int GetRandomRewardIndex()
        {
            return Random.Range(0, wheelSettings.WheelSlotCountValue);
        }
        
        private void OnWheelSpinComplete()
        {
            ObserverManager.Notify(new WheelSpinCompletedEvent(_selectedReward.reward, _selectedReward.amount));
        }
        
        private void HandleRewardDetermined(RewardDeterminedEvent evt)
        {
            ScheduleRewardWindowTask();
            ScheduleInitializeWheelTask(evt.ZoneData);
            
            _currentZoneData = evt.ZoneData;
            _currentZoneIndex = evt.CurrentZoneIndex;
        }
        
        private void HandleDeadZoneReward(DeadZoneRewardEvent evt)
        {
            ScheduleDeadZoneWindowTask();
            ScheduleInitializeWheelTask(_currentZoneData);
        }
        
        private void ScheduleRewardWindowTask()
        {
            var task = new RewardWindowTask(async () =>
            {
                var info = new RewardWindowCustomInfo(_selectedReward.reward, _selectedReward.amount);
                ObserverManager.Notify(new WindowStateChangedEvent(new WindowStateChangeInfo(WindowType.RewardWindow, true, info)));
            });
            
            _taskService.AddTask(task);
        }
        
        private void ScheduleDeadZoneWindowTask()
        {
            var task = new DeadZoneWindowTask(async () =>
            {
                var info = new DeadZoneWindowCustomInfo(_selectedReward.reward);
                ObserverManager.Notify(new WindowStateChangedEvent(new WindowStateChangeInfo(WindowType.DeadZoneWindow, true, info)));
            });
            
            _taskService.AddTask(task);
        }
        
        private void ScheduleInitializeWheelTask(ZoneData zoneData)
        {
            var task = new InitializeWheelTask(async () => InitializeWheelSession(zoneData));
            _taskService.AddTask(task);
        }
    }
}