using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace VertigoGames.Utility
{
    public static class Extensions
    {
        #region Component

        public static void Show(this Component component)
        {
            component.gameObject.SetActive(true);
        }

        public static void Hide(this Component component)
        {
            component.gameObject.SetActive(false);
        }

        #endregion

        #region RectTransform

        public static void SetAnchorPosX(this RectTransform rectTransform, float posX)
        {
            Vector2 anchorPos = rectTransform.anchoredPosition;
            anchorPos.x = posX;
            rectTransform.anchoredPosition = anchorPos;
        }

        public static void SetAnchorPosY(this RectTransform rectTransform, float posY)
        {
            Vector2 anchorPos = rectTransform.anchoredPosition;
            anchorPos.y = posY;
            rectTransform.anchoredPosition = anchorPos;
        }

        #endregion


        #region DoTween

        public static Tween DoBump(this Transform transform, float scaleFactor = 1.05f, float duration = 0.05f)
        {
            Vector3 scale = transform.localScale;

            Sequence seq = DOTween.Sequence();

            seq.Append(transform.DOScale(scale * scaleFactor, duration));
            seq.Append(transform.DOScale(scale, duration));

            return seq;
        }

        public static Tween DoBumpFromInitialScale(this Transform transform, float startScaleFactor = 0.4f,
            float scaleFactor = 1.05f, float duration = 0.5f)
        {
            Vector3 scale = Vector3.one;
            transform.localScale = Vector3.one * startScaleFactor;
            Sequence seq = DOTween.Sequence();

            seq.Append(transform.DOScale(scale * scaleFactor, duration));

            return seq;
        }

        public static Tween DoBumpFromInitialScale(this Transform transform, float startScaleFactor = 0.4f,
            float scaleUpFactor = 1.05f, float scaleDownFactor = 0.75f,
            float duration = 0.5f)
        {
            Vector3 scale = Vector3.one;
            transform.localScale = Vector3.one * startScaleFactor;
            Sequence seq = DOTween.Sequence();

            seq.Append(transform.DOScale(scale * scaleUpFactor, duration));
            seq.Append(transform.DOScale(scale * scaleDownFactor, duration));

            return seq;
        }

        #endregion

    }

}