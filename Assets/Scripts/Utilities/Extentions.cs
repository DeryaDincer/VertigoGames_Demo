using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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


        #region Image
        public static void SetAlpha(this Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
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
        
        public static Tween DoCount(this TextMeshProUGUI text, int startAmount, int endAmount, float duration = .5f)
        {
            int currentAmount = startAmount;
            return DOTween.To(
                () => currentAmount, 
                x => {
                    currentAmount = x;
                    text.text = currentAmount.ToString();
                },
                endAmount, 
                duration 
            ).SetId(text.transform); 
        }
        #endregion
    }
}