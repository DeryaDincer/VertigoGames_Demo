using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
using VertigoGames.UI.Button;
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
        [SerializeField] private WheelSettings wheelSettings;
        [SerializeField] private SpinButton spinButton;

        private (RewardData reward, int amount) _selectedReward;
        private WheelAnimationController _animationController;
        private WheelVisualController _visualController;
        private RewardSelectController _rewardSelectController;
        private ZoneData _currentZoneData;
        private int _currentZoneIndex;
        private readonly List<WheelItem> _wheelItems = new();
        private ObjectPoolManager _objectPoolManager;
        private ITaskService _taskService;
        
        public void Initialize(ObjectPoolManager poolManager, ITaskService taskService)
        {
            _objectPoolManager = poolManager;
            _taskService = taskService;
            
            _animationController = new WheelAnimationController(wheelContainer, indicatorImage.rectTransform, wheelSettings);
            _visualController = new WheelVisualController(spinWheelImage, indicatorImage);
            _rewardSelectController = new RewardSelectController();
            spinButton.SetWheelController(this);
        }

        #region Registration and Unregistration
        public void Register()
        {
            ObserverManager.Register<RewardDeterminedEvent>(HandleRewardDetermined);
            ObserverManager.Register<DeadZoneRewardEvent>(HandleDeadZoneReward);
        }

        public void Unregister()
        {
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
            ClearWheelItems();
            _animationController.ResetWheel();
            _currentZoneData = zoneData;
            
            List<RewardData> selectedRewards = _rewardSelectController.SelectRewards(zoneData, wheelSettings.WheelSlotCountValue);
            StartCoroutine(SpawnWheelItemsWithAnimation(selectedRewards));
            
            WheelZoneAppearanceInfo appearanceInfo = wheelSettings.GetWheelZoneAppearanceByZoneType(zoneData.ZoneType);
            _visualController.SetWheelVisual(appearanceInfo);
        } 
        
        private IEnumerator SpawnWheelItemsWithAnimation(List<RewardData> rewards)
        {
            yield return new WaitForSeconds(wheelSettings.WheelSpawnDelayValue);
            
            foreach (var (reward, index) in rewards.Select((data, i) => (data, i)))
            {
               WheelItem item = _objectPoolManager.GetObjectFromPool<WheelItem>(wheelItemContainer, Vector3.one);
                int rewardAmount = CalculateRewardAmount(reward);
                item.SetItem(reward, rewardAmount, index, wheelSettings.WheelRadiusValue, wheelSettings.WheelSlotCountValue);
                _wheelItems.Add(item);

                yield return new WaitForSeconds(wheelSettings.WheelSpawnDelayBetweenItemsValue); 
            }
            
            ObserverManager.Notify(new InputBlockStateChangedEvent(false));
            _taskService.CompleteTask(TaskType.InitializeWheel);
        }

        private void ClearWheelItems()
        {
            _wheelItems.ForEach(item => _objectPoolManager.ReturnToPool(item));
            _wheelItems.Clear(); 
        }
        
        private int CalculateRewardAmount(RewardData reward)
        {
            return reward.RewardInfo.InitialRewardCount * (_currentZoneIndex + 1);
        }
        
        public void SpinWheel()
        {
            ObserverManager.Notify(new InputBlockStateChangedEvent(true));
            int targetIndex = GetRandomRewardIndex();
            RewardData reward = _wheelItems[targetIndex].RewardData;
            _selectedReward = (reward, _wheelItems[targetIndex].RewardAmount);
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