using DG.Tweening;
using UnityEngine;
using VertigoGames.Settings;

public class RewardWindowAnimationController
{
    private readonly RectTransform _cardRoot;
    private readonly CanvasGroup _cardCanvasGroup;
    private readonly RectTransform _cardFrontValue;
    private readonly RectTransform _cardBackValue;
    private readonly RewardWindowSettings _settings;

    public RewardWindowAnimationController(
        RewardWindowSettings settings,
        RectTransform cardRoot,
        CanvasGroup cardCanvasGroup,
        RectTransform cardFrontValue,
        RectTransform cardBackValue
        )
    {
        _cardRoot = cardRoot;
        _cardCanvasGroup = cardCanvasGroup;
        _cardFrontValue = cardFrontValue;
        _cardBackValue = cardBackValue;
        _settings = settings;
    }

    public Tween PlayCardAnimation()
    {
        Sequence sequence = DOTween.Sequence();

        InitializeCardAnimation();

        sequence.Append(FadeInCard());
        sequence.Join(ScaleCard());
        sequence.Join(RotateCardToDefaultPosition());
        sequence.AppendInterval(0.1f);
        sequence.Append(FlipCardFrontToBack());
        sequence.AppendCallback(SwitchCardFaces);
        sequence.Append(FlipCardBackToFront());
        sequence.Append(BumpCard());
        sequence.AppendInterval(_settings.DelayBeforeClose);
        sequence.Append(FadeOutCard());

        return sequence;
    }

    private void InitializeCardAnimation()
    {
        _cardFrontValue.gameObject.SetActive(true);
        _cardBackValue.gameObject.SetActive(false);
        _cardBackValue.transform.rotation = Quaternion.identity;
        _cardRoot.transform.rotation = Quaternion.identity;
        _cardBackValue.Rotate(0, _settings.InitialCardBackRotationY, 0);
        _cardCanvasGroup.alpha = 0;
        _cardRoot.transform.localScale = Vector3.one * _settings.InitialCardScale;
        _cardRoot.Rotate(0, 0, _settings.InitialCardRotationZ);
    }

    private Tween FadeInCard()
    {
        return _cardCanvasGroup.DOFade(1, _settings.FadeDuration).SetEase(Ease.Linear);
    }

    private Tween ScaleCard()
    {
        return _cardRoot.DOScale(1, _settings.ScaleDuration).SetEase(Ease.Linear);
    }

    private Tween RotateCardToDefaultPosition()
    {
        return _cardRoot.DORotate(Vector3.zero, _settings.RotateDuration).SetEase(Ease.Linear);
    }

    private Tween FlipCardFrontToBack()
    {
        return _cardFrontValue.DORotate(new Vector3(0, _settings.InitialCardBackRotationY, 0), _settings.CardFlipDuration).SetEase(Ease.InSine);
    }

    private void SwitchCardFaces()
    {
        _cardFrontValue.gameObject.SetActive(false);
        _cardBackValue.gameObject.SetActive(true);
    }

    private Tween FlipCardBackToFront()
    {
        return _cardBackValue.transform.DORotate(Vector3.zero, _settings.CardFlipDuration).SetEase(Ease.OutSine);
    }

    private Tween BumpCard()
    {
        return _cardBackValue.transform.DOScale(_settings.BumpScale , _settings.BumpDuration).SetEase(Ease.OutSine);
    }

    private Tween FadeOutCard()
    {
        return _cardCanvasGroup.DOFade(0, _settings.FadeDuration).SetEase(Ease.Linear);
    }

    public void ResetCardRotation()
    {
        _cardBackValue.rotation = Quaternion.Euler(0, 0, 0);
        _cardFrontValue.rotation = Quaternion.Euler(0, 0, 0);
    }
}