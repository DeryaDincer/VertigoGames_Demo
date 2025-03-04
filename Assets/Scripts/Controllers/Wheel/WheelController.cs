using System;
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
    public class WheelController : MonoBehaviour , IRegisterable
    {
        [SerializeField] private RectTransform _wheelContainer;
        [SerializeField] private Image _spinWheelImageValue;
        [SerializeField] private Image _indicatorWheelImageValue;
        [SerializeField] private RectTransform _wheelItemContainer;
        [SerializeField] private WheelItem _wheelItemPrefab;
        [SerializeField] private WheelSettings _wheelSettings;
        [SerializeField] private SpinButton _spinButton;

        private (RewardData, int) _selectedReward;
        private WheelAnimationController _wheelAnimationController;
        private WheelVisualController _wheelVisualController;
        private RewardSelectController _rewardSelectController;
        private ZoneData _zoneData;
        private int _currentZoneIndex;
        private List<WheelItem> _wheelItems = new();
        private ObjectPoolManager _objectPoolManager;
        private TaskService _taskService;
        
        public void Initialize(ObjectPoolManager objectPoolManager, TaskService taskService)
        {
            _objectPoolManager = objectPoolManager;
            _taskService = taskService;
            
            _wheelAnimationController = new WheelAnimationController(_wheelContainer, _indicatorWheelImageValue.rectTransform, _wheelSettings);
            _wheelVisualController = new WheelVisualController(_spinWheelImageValue, _indicatorWheelImageValue);
            _rewardSelectController = new RewardSelectController();
            _spinButton.SetWheelController(this);
        }

        public void Deinitialize()
        {
            
        }

        public void Register()
        {
            ObserverManager.Register<OnRewardDetermined>(OnRewardDetermined);
        }

        public void Unregister()
        {
            ObserverManager.Unregister<OnRewardDetermined>(OnRewardDetermined);
        }

        public void BeginGameSession(ZoneData zoneData)
        {
            InitialWheelSession(zoneData);
        }
        
        private void InitialWheelSession(ZoneData zoneData)
        {
            DestroyAllItems();
            _wheelAnimationController.ResetWheelAnimation();
            _zoneData = zoneData;
            
            List<RewardData> selectedRewards = _rewardSelectController.SelectRewards(_zoneData, _wheelSettings.WheelSlotCountValue);//zonemanager yapsim
            StartCoroutine(InstantiateWheelItemsWithAnimation(selectedRewards, 0));
            
            WheelZoneAppearanceInfo wheelZoneAppearanceInfo = _wheelSettings.GetWheelZoneAppearanceByZoneType(_zoneData.ZoneType);
            _wheelVisualController.SetWheelVisual(wheelZoneAppearanceInfo);
        } 
        
        private IEnumerator InstantiateWheelItemsWithAnimation(List<RewardData> selectedRewards, int currentZoneIndex)
        {
            yield return new WaitForSeconds(_wheelSettings.WheelSpawnDelayValue);
            
            foreach (var (rewardData, index) in selectedRewards.Select((data, i) => (data, i)))
            {
               WheelItem item = _objectPoolManager.GetObjectFromPool<WheelItem>(_wheelItemContainer, Vector3.one);
         
                int rewardAmount = CalculateRewardAmount(rewardData);
                item.SetItem(rewardData, rewardAmount, index, _wheelSettings.WheelRadiusValue, _wheelSettings.WheelSlotCountValue);
                _wheelItems.Add(item);

                yield return new WaitForSeconds(_wheelSettings.WheelSpawnDelayBetweenItemsValue); 
            }
            
            ObserverManager.Notify(new InputBlockerEvent(false));
            _taskService.CompleteTask(TaskType.InitializeWheel);
        }

        private void DestroyAllItems()
        {
            _wheelItems.ForEach(item => _objectPoolManager.ReturnToPool(item));
            _wheelItems.Clear(); 
        }
        
        private int CalculateRewardAmount(RewardData rewardData)
        {
            return rewardData.RewardInfo.InitialRewardCount * (_currentZoneIndex + 1);
        }
        
        public void OnSpinWheel()
        {
            ObserverManager.Notify(new InputBlockerEvent(true));
            int targetRewardIndex = GetRandomItemIndex();
            RewardData rewardData = _wheelItems[targetRewardIndex].RewardData;
            _selectedReward = (rewardData, _wheelItems[targetRewardIndex].RewardAmount);
            _wheelAnimationController.SpinWheel(targetRewardIndex, SpinedWheel);
        }
        
        private int GetRandomItemIndex()
        {
            return Random.Range(0, _wheelSettings.WheelSlotCountValue);
        }
        
        private void SpinedWheel()
        {
            ObserverManager.Notify(new OnWheelSpinCompletedEvent(_selectedReward.Item1,_selectedReward.Item2));
        }
        
        private void OnRewardDetermined(OnRewardDetermined obj)
        {
            // if (obj.RewardData.RewardInfo.RewardType == RewardType.Bomb)
            // {
            //     AddDangerRewardWindowTask();
            //
            // }
            // else
            // {
            //     AddRewardWindowTask();
            //
            // }
            AddRewardWindowTask();
            AddInitializeWheelTask(obj.ZoneData);
            
            _currentZoneIndex = obj.CurrentZoneIndex;

            // WheelZoneAppearanceInfo wheelZoneAppearanceInfo = _wheelSettings.GetWheelZoneAppearanceByZoneType(obj.ZoneData.ZoneType);
            // _wheelVisualController.SetWheelVisual(wheelZoneAppearanceInfo);
        }
        
        private void AddRewardWindowTask()
        {
            var rewardWindowTask = new RewardWindowTask(async () =>
            {
                RewardWindowCustomInfo customInfo = new RewardWindowCustomInfo(_selectedReward.Item1, _selectedReward.Item2);
            
                WindowStateChangeInfo windowStateChangeInfo = new WindowStateChangeInfo(WindowType.RewardWindow, true, customInfo);
                ObserverManager.Notify(new WindowStateChangeEvent(windowStateChangeInfo));
            });
            
            _taskService.AddTask(rewardWindowTask);
        }
        
        private void AddDangerRewardWindowTask()
        {
            var dangerRewardWindowTask = new DangerRewardWindowTask(async () =>
            {
                RewardWindowCustomInfo customInfo = new RewardWindowCustomInfo(_selectedReward.Item1, _selectedReward.Item2);
            
                WindowStateChangeInfo windowStateChangeInfo = new WindowStateChangeInfo(WindowType.DangerRewardWindow, true, customInfo);
                ObserverManager.Notify(new WindowStateChangeEvent(windowStateChangeInfo));
            });
            
            _taskService.AddTask(dangerRewardWindowTask);
        }
        
        private void AddInitializeWheelTask(ZoneData zoneData)
        {
            var initializeWheelTask = new InitializeWheelTask(async () =>
            {
                InitialWheelSession(zoneData);
            });
            
            _taskService.AddTask(initializeWheelTask);
        }
    }
}
