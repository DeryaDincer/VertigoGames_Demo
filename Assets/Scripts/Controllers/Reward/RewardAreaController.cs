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
using VertigoGames.UI.Item.Reward;

namespace VertigoGames.Controllers.Reward
{
    public class RewardAreaController : MonoBehaviour, IRegisterable
    {
        [SerializeField] private RectTransform rewardItemContainer;

        private readonly List<RewardAreaItem> _rewardAreaItems = new();
        private ObjectPoolManager _objectPoolManager;
        private TaskService _taskService;
        private CurrencyManager _currencyManager;

        public void Initialize(ObjectPoolManager objectPoolManager, TaskService taskService, CurrencyManager currencyManager)
        {
            _objectPoolManager = objectPoolManager;
            _taskService = taskService;
            _currencyManager = currencyManager;
        }
        
        #region Registration
        public void Register() => ObserverManager.Register<RewardDeterminedEvent>(HandleRewardDetermined);
        public void Unregister() => ObserverManager.Unregister<RewardDeterminedEvent>(HandleRewardDetermined);
        #endregion

        public void BeginGameSession() => ResetSession();
        
        private void ResetSession() => DestroyAllRewardItems();

        private void DestroyAllRewardItems()
        {
            foreach (var item in _rewardAreaItems)
            {
                _objectPoolManager.ReturnToPool(item);
            }
            _rewardAreaItems.Clear();
        }

        private void HandleRewardDetermined(RewardDeterminedEvent rewardEvent)
        {
            if (rewardEvent.RewardData.RewardInfo.RewardType == RewardType.Bomb)
                return;

            var rewardTask = new RewardAreaTask(async () =>
            {
                _currencyManager.ModifyCurrency(rewardEvent.RewardData.RewardInfo.RewardType, rewardEvent.RewardAmount);
                await CreateOrUpdateRewardItemAsync(rewardEvent.RewardData, rewardEvent.RewardAmount);
            });

            _taskService.AddTask(rewardTask);
        }

        private async Task CreateOrUpdateRewardItemAsync(RewardData rewardData, int rewardAmount)
        {
            var item = GetOrCreateRewardItem(rewardData);
            item.SetItem(rewardData);
            _rewardAreaItems.Add(item);

            await Task.Delay(100);

            NotifyAnimationStarted(rewardData, rewardAmount, item);
        }

        private RewardAreaItem GetOrCreateRewardItem(RewardData rewardData)
        {
            var existingItem = _rewardAreaItems.Find(item => item.RewardData.RewardInfo.RewardType == rewardData.RewardInfo.RewardType);
            return existingItem ?? CreateNewRewardItem();
        }

        private RewardAreaItem CreateNewRewardItem()
        {
            var newItem = _objectPoolManager.GetObjectFromPool<RewardAreaItem>(rewardItemContainer, Vector3.one);
            newItem.transform.localScale = Vector3.zero;
            return newItem;
        }

        private void NotifyAnimationStarted(RewardData rewardData, int rewardAmount, RewardAreaItem item)
        {
            var animationInfo = new UIRewardAnimationInfo(rewardData, rewardAmount, Vector2.zero, item.transform.position, () => OnAnimationComplete(item));
            ObserverManager.Notify(new RewardAnimationStartedEvent(animationInfo));
        }

        private void OnAnimationComplete(RewardAreaItem item)
        {
            item.transform.localScale = Vector3.one;
            _taskService.CompleteTask(TaskType.RewardArea);
        }
    }
}