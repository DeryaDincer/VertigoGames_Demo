using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using VertigoGames.Datas.Reward;
using VertigoGames.Pooling;
using VertigoGames.Utility;

public class UIAnimationItem : PoolObject
{
    public RewardData RewardData => _rewardData;
    public int RewardAmount => _rewardAmount;

    [SerializeField] protected RectTransform _itemRoot;
    [SerializeField] protected Image _itemImageValue;

    private const float BumpDuration = 0.4f;
    private const float PathDurationBase = 0.6f;
    private const float PathDurationMultiplier = 0.002f;
    private const float MiddlePositionYOffset = 50f;
    private const float InitialScale = 1f;
    private const float TargetScale = 0.6f;

    private RewardData _rewardData;
    private int _rewardAmount;

    public void SetItem(RewardData rewardData)
    {
        SetRewardData(rewardData);
        SetItemSprite();
    }

    public virtual void SetRewardData(RewardData rewardData)
    {
        _rewardData = rewardData;
    }

    public virtual void SetItemSprite()
    {
        _itemImageValue.sprite = _rewardData.RewardInfo.Icon;
    }

    public Tween PlayAnimation(float delay, Vector3 startPosition, Vector3 targetPosition, System.Action onComplete)
    {
        Sequence seq = DOTween.Sequence();

        Vector3 middlePosition = (startPosition + targetPosition) / 2;
        middlePosition.y += MiddlePositionYOffset;
        float duration = PathDurationBase;

        Vector3[] path = { startPosition, middlePosition, targetPosition };

        transform.localScale = Vector3.one * InitialScale;
        seq.AppendInterval(delay);
        seq.Append(transform.DoBump());
        seq.Append(transform.DOPath(path, duration, PathType.CatmullRom).SetEase(Ease.InSine));
        seq.Join(transform.DOScale(TargetScale, duration).SetEase(Ease.InSine));
        seq.AppendCallback(() => onComplete?.Invoke());
        return seq;
    }

    public override void OnDeactivate()
    {
        
    }

    public override void OnSpawn()
    {
       
    }

    public override void OnCreated()
    {
       
    }
}