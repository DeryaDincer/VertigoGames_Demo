using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Managers;
using VertigoGames.TaskService;

namespace VertigoGames.Controllers.Zone
{
    public class RewardAreaController : MonoBehaviour, IInitializable, IRegisterable
    {
        [SerializeField] private RewardAreaItem _rewardAreaItemPrefab;
        [SerializeField] private RectTransform _rewardItemContainer;
        private List<RewardAreaItem> _rewardAreaItems = new();
        
        public void Initialize()
        {
           
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

        private void OnRewardDetermined(OnRewardDetermined obj)
        {
            var rewardAreaTask = new RewardAreaTask(async () =>
            {
                InstantiateRewardAreaItem(obj.RewardData, obj.RewardAmount);
            });
            
            TaskService.TaskService.Instance.AddTask(rewardAreaTask);
        }

        private void InstantiateRewardAreaItem(RewardData rewardData, int rewardAmount)
        {
            var item = Instantiate(_rewardAreaItemPrefab, _rewardItemContainer);
            item.SetItem(rewardData, rewardAmount);
            _rewardAreaItems.Add(item);
        }
    }
}

