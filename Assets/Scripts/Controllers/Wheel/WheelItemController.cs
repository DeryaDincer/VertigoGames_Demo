using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VertigoGames.Datas.Reward;
using VertigoGames.Pooling;
using VertigoGames.Settings;
using VertigoGames.UI.Item.Wheel;

namespace VertigoGames.Controllers.Wheel
{
    public class WheelItemController 
    {
        private readonly ObjectPoolManager _objectPoolManager;
        private readonly WheelSettings _settings;
        
        private readonly RectTransform _wheelItemContainer;
        private readonly List<WheelItem> _wheelItems = new();
        public WheelItemController(ObjectPoolManager objectPoolManager, WheelSettings settings, RectTransform wheelItemContainer)
        {
            _objectPoolManager = objectPoolManager;
            _settings = settings;
            _wheelItemContainer = wheelItemContainer;
        }
        
        public IEnumerator SpawnWheelItemsWithAnimation(List<RewardData> rewards, int currentZoneIndex, Action completedAction)
        {
            yield return new WaitForSeconds(_settings.WheelSpawnDelayValue);
            
            foreach (var (reward, index) in rewards.Select((data, i) => (data, i)))
            {
                WheelItem item = _objectPoolManager.GetObjectFromPool<WheelItem>(_wheelItemContainer, Vector3.one);
                int rewardAmount = CalculateRewardAmount(reward, currentZoneIndex);
                item.SetItem(reward, rewardAmount, index, _settings.WheelRadiusValue, _settings.WheelSlotCountValue);
                _wheelItems.Add(item);

                yield return new WaitForSeconds(_settings.WheelSpawnDelayBetweenItemsValue); 
            }
            
            completedAction?.Invoke();
        }
        
        private int CalculateRewardAmount(RewardData reward, int currentZoneIndex)
        {
            return reward.RewardInfo.InitialRewardCount * (currentZoneIndex + 1);
        }
        
        public void ClearWheelItems()
        {
            _wheelItems.ForEach(item => _objectPoolManager.ReturnToPool(item));
            _wheelItems.Clear(); 
        }

        public WheelItem GetWheelItem(int targetIndex)
        {
            return _wheelItems[targetIndex];
        }
    }  
}

