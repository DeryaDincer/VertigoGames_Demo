using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.Managers;
using VertigoGames.Settings;
using VertigoGames.UI.Window;

namespace VertigoGames.UI.Window
{
    public class RewardWindowController : Window
    {
        [Header("UI References")]
        [SerializeField] private Image _rewardIcon;
        [SerializeField] private TextMeshProUGUI _rewardTitleValue;
        [SerializeField] private TextMeshProUGUI _rewardAmountValue;
        [SerializeField] private RectTransform _cardRoot;
        [SerializeField] private CanvasGroup _cardCanvasGroup;
        [SerializeField] private RectTransform _cardFrontValue;
        [SerializeField] private RectTransform _cardBackValue;

        [Header("Reward Window Settings")]
        [SerializeField] private RewardWindowSettings _rewardWindowSettings;

        private RewardWindowCustomData _rewardWindowCustomData;
        private RewardWindowAnimationController _animationController;

        public override WindowType WindowType => WindowType.RewardWindow;

        private void Awake()
        {
            // AnimationController'ı başlat
            _animationController = new RewardWindowAnimationController(
                _rewardWindowSettings,
                _cardRoot,
                _cardCanvasGroup,
                _cardFrontValue,
                _cardBackValue
            );
        }

        public override void OnWindowActivated(object customData)
        {
            base.OnWindowActivated(customData);
            _rewardWindowCustomData = customData as RewardWindowCustomData;

            if (_rewardWindowCustomData == null)
            {
                Debug.LogError("RewardWindowCustomData is null!");
                return;
            }

            SetRewardInfo(_rewardWindowCustomData.RewardData);
            _animationController.PlayCardAnimation().OnComplete(CloseWindow);
        }

        public override void OnWindowDeactivated()
        {
            DOTween.Kill(transform);
            base.OnWindowDeactivated();
            _animationController.ResetCardRotation();
            _cardCanvasGroup.alpha = 1;
        }

        private void SetRewardInfo(RewardData rewardData)
        {
            _rewardIcon.sprite = rewardData.RewardInfo.Icon;
            _rewardTitleValue.text = rewardData.RewardInfo.Title;
            _rewardAmountValue.text = "x" + 3; 
        }

        private void CloseWindow()
        {
            WindowActivateData windowActivateData = new WindowActivateData
            {
                WindowType = WindowType.RewardWindow,
                ActiveStatus = false,
                CustomData = null
            };

            ObserverManager.Notify(new WindowActivateDataEvent(windowActivateData));
        }
    }
}

public class RewardWindowCustomData
{
    public RewardData RewardData;
}