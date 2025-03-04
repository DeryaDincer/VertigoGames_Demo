using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Events;
using VertigoGames.Managers;
using VertigoGames.Settings;
using VertigoGames.Utility;

namespace VertigoGames.UI.Window
{
    public class DeadZoneWindowAnimationController
    {
        private readonly RectTransform _cardRoot;
        private readonly DeadZoneWindowSettings _settings;
        private readonly Image _windowBackgroundImage;

        public DeadZoneWindowAnimationController(
            DeadZoneWindowSettings settings,
            RectTransform cardRoot,
            Image windowBackgroundImage
        )
        {
            _settings = settings;
            _cardRoot = cardRoot;
            _windowBackgroundImage = windowBackgroundImage;
        }

        public Tween PlayCardAnimation()
        {
            Sequence sequence = DOTween.Sequence();

            InitializeCardAnimation();

            sequence.Append(FadeInBackground());
            sequence.Join(MoveCard());
            sequence.Append(RotateCardToDefaultPosition());
            sequence.AppendInterval(_settings.DelayBeforeClose);
            sequence.AppendCallback(() => ObserverManager.Notify(new InputBlockerEvent(false)));
            return sequence;
        }

        private void InitializeCardAnimation()
        {
            _windowBackgroundImage.SetAlpha(0);
            _cardRoot.Rotate(0, 0, _settings.InitialCardBackRotationZ);
            _cardRoot.transform.localPosition = new Vector3(0, _settings.InitialCardPositionY, 0); //screen heigtha gore olabilir
        }

        private Tween FadeInBackground()
        {
            return _windowBackgroundImage.DOFade(_settings.BackgroundFadeValue, _settings.BackgroundFadeDuration).SetEase(Ease.Linear);
        }

        private Tween MoveCard()
        {
            return _cardRoot.DOLocalMove(Vector3.zero, _settings.ScaleDuration).SetEase(Ease.Linear);
        }

        private Tween RotateCardToDefaultPosition()
        {
            return _cardRoot.DORotate(Vector3.zero, _settings.RotateDuration).SetEase(Ease.Linear);
        }


        public void ResetCardRotation()
        {
            _cardRoot.rotation = Quaternion.Euler(0, 0, 0);
            _windowBackgroundImage.SetAlpha(0);
        }
    }
}
