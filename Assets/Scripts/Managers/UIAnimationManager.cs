using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Utility;
using Random = UnityEngine.Random;

namespace VertigoGames.Managers
{
    public class UIAnimationManager : MonoBehaviour,  IInitializable, IRegisterable
    {
        public UIAnimationItem _animationItemPrefab;

        private RewardAnimation _rewardAnimation;
        public Transform target; 
        public float spawnRadius = 30f; // Rewardların oluşacağı çemberin yarıçapı
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
            _rewardAnimation = obj.RewardAnimation;
            target.position = _rewardAnimation.EndAnimationTransform;
            SpawnRewards();
        }

        private void  SpawnRewards()
        {
            RewardData rewardData = _rewardAnimation.RewardData;

            Sequence seq = DOTween.Sequence();
            
            for (int i = 0; i < 4; i++)
            {
                Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
                randomPosition.z = 0; 
                UIAnimationItem reward = Instantiate(_animationItemPrefab, randomPosition,Quaternion.identity);
                reward.transform.parent = transform;
                reward.SetItem(rewardData);
                _rewardAnimation.StartAnimationTransform = randomPosition;
             
                seq.Join(MoveToTarget(reward));
              //  seq.AppendInterval(50);
            }

            seq.AppendCallback(() =>
            {
                _rewardAnimation.AnimationCompleteAction?.Invoke();
            });
        }

        private Tween MoveToTarget(UIAnimationItem reward)
        {
            return PlayRewardImageAnimations(reward).OnComplete(() =>
            {
                Destroy(reward.gameObject);
            });
        }

         private Tween PlayRewardImageAnimations(UIAnimationItem reward)
        {
            Sequence seq = DOTween.Sequence();

            Vector3 middlePosition = (_rewardAnimation.StartAnimationTransform + target.position) / 2;


            float distance = Math.Abs(reward.transform.position.y - _rewardAnimation.StartAnimationTransform.y);
            float duration = .4f + (int)distance * .02f;

            Vector3[] path = { _rewardAnimation.StartAnimationTransform, middlePosition, target.position };

            reward.transform.localScale = Vector3.zero;
            seq.Append(RewardImageBumpAnimation(reward.transform));
            
            seq.Append(reward.transform.DOPath(path, duration, PathType.CatmullRom).SetEase(Ease.InSine).OnComplete(() =>
            {
               
            }));
            seq.Join(reward.transform.DOScale( 1f, .45f).SetEase(Ease.InSine));
            seq.Append(RewardImageBumpAnimation(reward.transform));
            seq.AppendCallback(() =>
            {
               
            });
            return seq;
        }
        protected Tween RewardImageBumpAnimation(Transform image, float scaleFactor = 1.15f, float duration = 0.17f)
        {
            float InitScale = 1;
            Sequence seq = DOTween.Sequence();
            seq.Append(image.transform.DOScale(InitScale * scaleFactor, duration).SetEase(Ease.InSine));
            seq.Append(image.transform.DOScale(InitScale * .85f, duration).SetEase(Ease.OutSine));
            seq.Append(image.transform.DOScale(InitScale, duration).SetEase(Ease.InSine));
            return seq;
        }
    }
}

public class RewardAnimation
{
    public RewardData RewardData;
    public int RewardAmount;
    public Vector3 StartAnimationTransform;
    public Vector3 EndAnimationTransform;
    public Action AnimationCompleteAction;

    public RewardAnimation(RewardData rewardData, int rewardAmount, Vector3 startAnimationTransform, 
        Vector3 endAnimationTransform, Action animationCompleteAction)
    {
        RewardData = rewardData;
        RewardAmount = rewardAmount;
        StartAnimationTransform = startAnimationTransform;
        EndAnimationTransform = endAnimationTransform;
        AnimationCompleteAction = animationCompleteAction;
    }
}

