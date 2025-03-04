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
            obj.RewardData.AddCurrency(obj.RewardAmount);
            
            var rewardAreaTask = new RewardAreaTask(async () =>
            {
                InstantiateRewardAreaItem(obj.RewardData, obj.RewardAmount);
            });
            
            TaskService.Instance.AddTask(rewardAreaTask);
        }

        private async void InstantiateRewardAreaItem(RewardData rewardData, int rewardAmount)
        {
            var item = Instantiate(_rewardAreaItemPrefab, _rewardItemContainer);
            item.SetItem(rewardData, rewardAmount);
            _rewardAreaItems.Add(item);

            await Task.Delay(100);
            
            RewardAnimation rewardAnimation =
                new RewardAnimation(rewardData, rewardAmount, Vector2.zero, item.transform.position, AnimationCompleted);
            ObserverManager.Notify(new OnRewardAnimationEvent(rewardAnimation));
      
        }

        private void AnimationCompleted()
        {
            TaskService.Instance.CompleteTask(TaskType.RewardArea);
        }
    }
}

