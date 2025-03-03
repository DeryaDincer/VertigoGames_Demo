using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Controllers.Reward;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Managers;
using VertigoGames.Settings;
using VertigoGames.TaskService;
using VertigoGames.UI.Button;
using VertigoGames.UI.Item.Wheel;
using Random = UnityEngine.Random;

namespace VertigoGames.Controllers.Wheel
{
    public class WheelController : MonoBehaviour , IInitializable, IRegisterable
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
        
        public void Initialize()
        {
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
            ObserverManager.Register<OnZoneStateReadyEvent>(OnZoneStateReady);
            ObserverManager.Register<OnRewardDetermined>(OnRewardDetermined);
        }

        public void Unregister()
        {
            ObserverManager.Unregister<OnZoneStateReadyEvent>(OnZoneStateReady);
            ObserverManager.Unregister<OnRewardDetermined>(OnRewardDetermined);
        }

        private void OnZoneStateReady(OnZoneStateReadyEvent obj)
        {
            DestroyAllItems();
            _wheelAnimationController.ResetWheelAnimation();
            _zoneData = obj.ZoneData;
            
            List<RewardData> selectedRewards = _rewardSelectController.SelectRewards(_zoneData, _wheelSettings.WheelSlotCountValue);
            StartCoroutine(InstantiateWheelItemsWithAnimation(selectedRewards, 0));
        }
        
        private IEnumerator InstantiateWheelItemsWithAnimation(List<RewardData> selectedRewards, int currentZoneIndex)
        {
            yield return new WaitForSeconds(_wheelSettings.WheelSpawnDelayValue);
            
            foreach (var (rewardData, index) in selectedRewards.Select((data, i) => (data, i)))
            {
                var item = Instantiate(_wheelItemPrefab, _wheelItemContainer);
                int rewardAmount = CalculateRewardAmount(rewardData);
                item.SetItem(rewardData, rewardAmount, index, _wheelSettings.WheelRadiusValue, _wheelSettings.WheelSlotCountValue);
                _wheelItems.Add(item);

                yield return new WaitForSeconds(_wheelSettings.WheelSpawnDelayBetweenItemsValue); 
            }
        }

        private void DestroyAllItems()
        {
            _wheelItems.ForEach(item => Destroy(item.gameObject));
            _wheelItems.Clear(); 
        }
        
        private int CalculateRewardAmount(RewardData rewardData)
        {
            return rewardData.RewardInfo.InitialRewardCount * (_currentZoneIndex + 1);
        }
        
        public void OnSpinWheel()
        {
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
            ObserverManager.Notify(new InputBlockerEvent(false));
            ObserverManager.Notify(new OnWheelSpinCompletedEvent(_selectedReward.Item1,_selectedReward.Item2));
        }
        
        private void OnRewardDetermined(OnRewardDetermined obj)
        {
            AddRewardWindowTask();

            _currentZoneIndex = obj.CurrentZoneIndex;
            WheelZoneAppearance wheelZoneAppearance = _wheelSettings.GetWheelZoneAppearanceByZoneType(obj.ZoneData.ZoneType);
            _wheelVisualController.SetWheelVisual(wheelZoneAppearance);
        }
        
        
        private void AddRewardWindowTask()
        {
            var rewardWindowTask = new RewardWindowTask(async () =>
            {
                int index = Random.Range(0, _wheelItems.Count);
                RewardWindowCustomData customData = new RewardWindowCustomData();
                customData.RewardData = _wheelItems[index].RewardData;
          
                WindowActivateData windowActivateData = new WindowActivateData(WindowType.RewardWindow, true, customData);
                ObserverManager.Notify(new WindowActivateDataEvent(windowActivateData));
            });
            
            TaskService.TaskService.Instance.AddTask(rewardWindowTask);
        }
        
    
    }
}
