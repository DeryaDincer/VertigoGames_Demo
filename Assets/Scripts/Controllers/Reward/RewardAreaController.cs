using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.GameTasks;
using VertigoGames.Interfaces;
using VertigoGames.Managers;
using VertigoGames.Pooling;
using VertigoGames.Services;
using VertigoGames.UI.Item.Wheel;
using VertigoGames.Utility;

namespace VertigoGames.Controllers.Zone
{
    public class RewardAreaController : MonoBehaviour, IRegisterable
    {
        [SerializeField] private RewardAreaItem _rewardAreaItemPrefab;
        [SerializeField] private RectTransform _rewardItemContainer;
        private List<RewardAreaItem> _rewardAreaItems = new();
        private ObjectPoolManager _objectPoolManager;
        private TaskService _taskService;
        
        public void Initialize(ObjectPoolManager objectPoolManager, TaskService taskService)
        {
            _objectPoolManager = objectPoolManager;
            _taskService = taskService;
        }
        
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
            _rewardAreaItems.ForEach(item => _objectPoolManager.ReturnToPool(item));
            _rewardAreaItems.Clear(); 
        }
        
        private void OnRewardDetermined(OnRewardDetermined obj)
        {
            var rewardAreaTask = new RewardAreaTask(async () =>
            {
                obj.RewardData.AddCurrency(obj.RewardAmount);
                InstantiateRewardAreaItem(obj.RewardData, obj.RewardAmount);
            });
            
            _taskService.AddTask(rewardAreaTask);
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
                item = _objectPoolManager.GetObjectFromPool<RewardAreaItem>(_rewardItemContainer, Vector3.one);
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
            _taskService.CompleteTask(TaskType.RewardArea);
        }
    }
}

