using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Controllers.Reward;
using VertigoGames.Datas.Reward;
using VertigoGames.Interfaces;
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
        private ZoneData zoneData;
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
           
        }

        public void Unregister()
        {
           
        }
        
        public void StartGame(ZoneData zoneData)
        {
            this.zoneData = zoneData;
            
            List<RewardData> selectedRewards = _rewardSelectController.SelectRewards(zoneData, _wheelSettings.WheelSlotCountValue);
            InstantiateWheelItems(selectedRewards);
        }
        
        private void InstantiateWheelItems(List<RewardData> selectedRewards)
        {
            for (int i = 0; i < selectedRewards.Count; i++)
            {
                var rewardSo = selectedRewards[i];
                var item = Instantiate(_wheelItem, _wheelItemContainer);
                 item.SetItem(rewardSo, 1, i, _wheelSettings.WheelRadiusValue);
                 items.Add(item);
            }
        }
    }
}
