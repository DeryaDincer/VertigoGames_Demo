using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using VertigoGames.Datas.Reward;
using VertigoGames.Pooling;
using VertigoGames.Settings;
using VertigoGames.Utility;

namespace VertigoGames.UI.Item.UIAnimation
{
    public class UIAnimationItem : PoolObject
    {
        [SerializeField] protected Image itemImageValue;

        private RewardData _rewardData;
        private int _rewardAmount;
        private UIAnimationSettings _animationSettings;

        public override void OnDeactivate() { }

        public override void OnSpawn() { }

        public override void OnCreated() { }

        public void SetItem(UIAnimationSettings animationSettings, RewardData rewardData)
        {
            _animationSettings = animationSettings;
            SetRewardData(rewardData);
            SetItemSprite();
        }

        protected virtual void SetRewardData(RewardData rewardData) => _rewardData = rewardData;

        protected virtual void SetItemSprite() => itemImageValue.sprite = _rewardData.RewardInfo.Icon;
        
        public Tween PlayAnimation(float delay, Vector3 startPosition, Vector3 targetPosition, System.Action onComplete)
        {
            Sequence seq = DOTween.Sequence();

            Vector3 middlePosition = (startPosition + targetPosition) / 2;
            middlePosition.y += _animationSettings.MiddlePositionYOffset;

            Vector3[] path = { startPosition, middlePosition, targetPosition };

            seq.AppendInterval(delay);
            seq.Append(transform.DoBump());
            seq.Append(transform.DOPath(path, _animationSettings.PathDurationBase, PathType.CatmullRom)
                .SetEase(Ease.InSine));
            seq.Join(transform.DOScale(_animationSettings.TargetScale, _animationSettings.PathDurationBase)
                .SetEase(Ease.InSine));
            seq.AppendCallback(() => onComplete?.Invoke());
            return seq;
        }
    }
}