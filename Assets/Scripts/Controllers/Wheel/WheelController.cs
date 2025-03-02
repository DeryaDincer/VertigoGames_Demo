using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Controllers.Reward;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Managers;
using VertigoGames.Settings;
using VertigoGames.UI.Item.Wheel;
using Random = UnityEngine.Random;

namespace VertigoGames.Controllers.Wheel
{
    public class WheelController : MonoBehaviour , IInitializable, IRegisterable
    {
        [SerializeField] private RectTransform _wheelContainer;
        [SerializeField] private Image _spinWheelImageValue;
        [SerializeField] private RectTransform _wheelItemContainer;
        [SerializeField] private WheelItem _wheelItem;
        [SerializeField] private WheelSettings _wheelSettings;
        
        private WheelAnimationController _wheelAnimationController;
        private RewardSelectController _rewardSelectController;
        private ZoneData _zoneData;
        private int _currentZoneIndex;
        private List<WheelItem> items = new List<WheelItem>();
        
        public void Initialize()
        {
            _rewardSelectController = new RewardSelectController();
            _wheelAnimationController = new WheelAnimationController(_wheelContainer, _wheelSettings);
        }

        public void Deinitialize()
        {
            
        }

        public void Register()
        {
            ObserverManager.Register<SpinWheelEvent>(OnSpinWheel);
            ObserverManager.Register<OnZoneStateReadyEvent>(OnZoneStateReady);
            ObserverManager.Register<OnUpdateZoneDataEvent>(OnUpdateZoneData);
        }

        public void Unregister()
        {
            ObserverManager.Unregister<SpinWheelEvent>(OnSpinWheel);
            ObserverManager.Unregister<OnZoneStateReadyEvent>(OnZoneStateReady);
            ObserverManager.Unregister<OnUpdateZoneDataEvent>(OnUpdateZoneData);
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
                var item = Instantiate(_wheelItem, _wheelItemContainer);
                int rewardAmount = CalculateRewardAmount(rewardData);
                item.SetItem(rewardData, rewardAmount, index, _wheelSettings.WheelRadiusValue);
                items.Add(item);

                yield return new WaitForSeconds(_wheelSettings.WheelSpawnDelayBetweenItemsValue); 
            }
        }

        private void DestroyAllItems()
        {
            items.ForEach(item => Destroy(item.gameObject));
            items.Clear(); 
        }
        
        private int CalculateRewardAmount(RewardData rewardData)
        {
            return rewardData.RewardInfo.InitialRewardCount * (_currentZoneIndex + 1);
        }
        
        private void OnSpinWheel(SpinWheelEvent obj)
        {
            _wheelAnimationController.SpinWheel(OpenRewardWindow);
        }
        
        private void OpenRewardWindow()
        {
          //new spin ready
          ObserverManager.Notify(new OnWheelSpinCompletedEvent());
        }
        
        private void OnUpdateZoneData(OnUpdateZoneDataEvent obj)
        {
            _currentZoneIndex = obj.CurrentZoneIndex;
        }
    }
}
