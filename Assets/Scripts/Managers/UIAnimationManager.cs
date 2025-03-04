using System;
using UnityEngine;
using DG.Tweening;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Pooling;
using VertigoGames.UI.Item.Wheel;
using VertigoGames.Utility;
using Random = UnityEngine.Random;

namespace VertigoGames.Managers
{
    public class UIAnimationManager : MonoBehaviour, IInitializable, IRegisterable
    {
        public UIAnimationItem _animationItemPrefab;
        private UIRewardAnimationInfo _uıRewardAnimationInfo;
        public Transform target;
        public float spawnRadius = 40f;

        private const float SpawnDelayMultiplier = 0.1f; 
        private const float ZPosition = 0f; 

        public void Initialize()
        {
        }

        public void Deinitialize()
        {
        }

        public void Register()
        {
            ObserverManager.Register<OnRewardAnimationEvent>(OnRewardAnimation);
        }

        public void Unregister()
        {
            ObserverManager.Unregister<OnRewardAnimationEvent>(OnRewardAnimation);
        }

        private void OnRewardAnimation(OnRewardAnimationEvent obj)
        {
            _uıRewardAnimationInfo = obj.UIRewardAnimationInfo;
            target.position = _uıRewardAnimationInfo.EndAnimationTransform;
            SpawnRewards(obj.UIRewardAnimationInfo.RewardAmount);
        }

        private void SpawnRewards(int rewardAmount)
        {
            RewardData rewardData = _uıRewardAnimationInfo.RewardData;

            Sequence seq = DOTween.Sequence();
            bool completeActionInvoked = false;
            rewardAmount = Math.Clamp(rewardAmount, 0, 15);
            for (int i = 0; i < rewardAmount; i++)
            {
                UIAnimationItem reward = ObjectPoolManager.Instance.GetObjectFromPool<UIAnimationItem>(transform, Vector3.one);

                Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
                randomPosition.z = ZPosition; 
                reward.transform.position = randomPosition;
                
                reward.SetItem(rewardData);
                _uıRewardAnimationInfo.StartAnimationTransform = randomPosition;

                float delay = i * SpawnDelayMultiplier; 
                seq.Join(reward.PlayAnimation(delay, randomPosition, target.position, () =>
                {
                    if (!completeActionInvoked)
                    {
                        _uıRewardAnimationInfo.AnimationCompleteAction?.Invoke();
                        completeActionInvoked = true;
                    }

                    ObjectPoolManager.Instance.ReturnToPool(reward);
                }));
            }
        }
    }
}