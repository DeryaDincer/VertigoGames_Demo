using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.GameTasks;
using VertigoGames.Interfaces;
using VertigoGames.Managers;
using VertigoGames.Services;
using VertigoGames.Utility;

namespace VertigoGames.Controllers.Zone
{
    public class RewardAreaController : MonoBehaviour, IRegisterable
    {
        [SerializeField] private RewardAreaItem _rewardAreaItemPrefab;
        [SerializeField] private RectTransform _rewardItemContainer;
        private List<RewardAreaItem> _rewardAreaItems = new();
        
        public void Register()
        {
            ObserverManager.Register<OnRewardDetermined>(OnRewardDetermined);
        }

        public void Unregister()
        {
            ObserverManager.Unregister<OnRewardDetermined>(OnRewardDetermined);
        }

        public void BeginGameSession()
        {
            DestroyAllItems();
        }
        
        private void DestroyAllItems()
        {
            _rewardAreaItems.ForEach(item => Destroy(item.gameObject));
            _rewardAreaItems.Clear(); 
        }
        
        private void OnRewardDetermined(OnRewardDetermined obj)
        {
            var rewardAreaTask = new RewardAreaTask(async () =>
            {
                obj.RewardData.AddCurrency(obj.RewardAmount);
                InstantiateRewardAreaItem(obj.RewardData, obj.RewardAmount);
            });
            
            TaskService.Instance.AddTask(rewardAreaTask);
        }

        private async void InstantiateRewardAreaItem(RewardData rewardData, int rewardAmount)
        {
            RewardAreaItem item = null;
            bool containItem = false;
            foreach (var rewardAreaItem in _rewardAreaItems)
            {
                if (rewardAreaItem.RewardData == rewardData)
                {
                    containItem = true;
                    item = rewardAreaItem;
                }
            }

            if (!containItem)
            {
                item = Instantiate(_rewardAreaItemPrefab, _rewardItemContainer);
                item.transform.localScale = Vector3.zero;
            }
            
            item.SetItem(rewardData, rewardAmount);
            _rewardAreaItems.Add(item);

            await Task.Delay(100);
            
            UIRewardAnimationInfo uıRewardAnimationInfo =
                new UIRewardAnimationInfo(rewardData, rewardAmount, Vector2.zero, 
                    item.transform.position,
                    () =>
                    {
                        AnimationCompleted(item, rewardData, rewardAmount);
                    });
            ObserverManager.Notify(new OnRewardAnimationEvent(uıRewardAnimationInfo));
        }
        
        private void AnimationCompleted(RewardAreaItem item , RewardData rewardData, int rewardAmount)
        {
            item.transform.localScale = Vector3.one;
            TaskService.Instance.CompleteTask(TaskType.RewardArea);
        }
    }
}

