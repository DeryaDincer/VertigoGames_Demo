using System;
using UnityEngine;
using DG.Tweening;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Pooling;
using VertigoGames.Settings;
using VertigoGames.UI.Item.UIAnimation;
using Random = UnityEngine.Random;

namespace VertigoGames.Managers
{
    /// <summary>
    /// Manages UI animations for rewards, including spawning and animating reward items.
    /// </summary>
    public class UIAnimationManager : MonoBehaviour, IRegisterable
    {
        [SerializeField] private UIAnimationSettings _animationSettings;
        private bool _isCompleteActionInvoked;
        private UIRewardAnimationInfo _uiRewardAnimationInfo;
        private ObjectPoolManager _objectPoolManager;


        #region Initialization 

        public void Initialize(ObjectPoolManager objectPoolManager)
        {
            _objectPoolManager = objectPoolManager;
        }

        #endregion

        #region Registration and Unregistration

        public void Register()
        {
            ObserverManager.Register<RewardAnimationStartedEvent>(OnRewardAnimation);
        }

        public void Unregister()
        {
            ObserverManager.Unregister<RewardAnimationStartedEvent>(OnRewardAnimation);
        }

        #endregion

        private void OnRewardAnimation(RewardAnimationStartedEvent rewardAnimationStartedEvent)
        {
            _uiRewardAnimationInfo = rewardAnimationStartedEvent.UIRewardAnimationInfo;
            SpawnRewards(_uiRewardAnimationInfo.RewardAmount);
        }

        #region Reward Spawning and Animation

        private void SpawnRewards(int rewardAmount)
        {
            RewardData rewardData = _uiRewardAnimationInfo.RewardData;
            Sequence animationSequence = DOTween.Sequence();
            _isCompleteActionInvoked = false; 

            rewardAmount = Math.Clamp(rewardAmount, 0, _animationSettings.MaxRewardCount);

            for (int i = 0; i < rewardAmount; i++)
            {
                float delay = i * _animationSettings.SpawnDelayMultiplier;
                SpawnAndAnimateReward(rewardData, delay, animationSequence);
            }
        }

        private void SpawnAndAnimateReward(RewardData rewardData, float delay, Sequence animationSequence)
        {
            UIAnimationItem rewardItem = GetRewardItemFromPool();
            Vector3 spawnPosition = GetRandomSpawnPosition();

            rewardItem.transform.position = spawnPosition;
            rewardItem.SetItem(_animationSettings, rewardData);
            _uiRewardAnimationInfo.StartAnimationTransform = spawnPosition;

            animationSequence.Join(rewardItem.PlayAnimation(delay, spawnPosition, _uiRewardAnimationInfo.EndAnimationTransform,
                () => OnRewardAnimationComplete(rewardItem)));
        }

        private void OnRewardAnimationComplete(UIAnimationItem rewardItem)
        {
            if (!_isCompleteActionInvoked)
            {
                _uiRewardAnimationInfo.AnimationCompleteAction?.Invoke();
                _isCompleteActionInvoked = true;
            }

            ReturnRewardItemToPool(rewardItem);
        }
      
        private UIAnimationItem GetRewardItemFromPool()
        {
            return _objectPoolManager.GetObjectFromPool<UIAnimationItem>(transform, Vector3.one);
        }

        private Vector3 GetRandomSpawnPosition()
        {
            return transform.position + Random.insideUnitSphere * _animationSettings.SpawnRadius;
        }

        private void ReturnRewardItemToPool(UIAnimationItem rewardItem)
        {
            _objectPoolManager.ReturnToPool(rewardItem);
        }

        #endregion
    }
}