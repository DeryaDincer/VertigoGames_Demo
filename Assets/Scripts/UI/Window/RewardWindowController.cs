using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Controllers.Reward;
using VertigoGames.Datas.Reward;
using VertigoGames.Managers;
using VertigoGames.Services;
using VertigoGames.Settings;

namespace VertigoGames.UI.Window
{
    public class RewardWindowController : BaseWindowController
    {
        [Header("UI References")]
        [SerializeField] private Image rewardIcon;
        [SerializeField] private TextMeshProUGUI rewardTitleText;
        [SerializeField] private TextMeshProUGUI rewardAmountText;
        [SerializeField] private RectTransform cardRoot;
        [SerializeField] private CanvasGroup cardCanvasGroup;
        [SerializeField] private RectTransform cardFront;
        [SerializeField] private RectTransform cardBack;

        [Header("Reward Window Settings")]
        [SerializeField] private RewardWindowSettings rewardWindowSettings;

        private RewardWindowCustomInfo _rewardWindowCustomInfo;
        private RewardWindowAnimationController _animationController;

        public override WindowType WindowType => WindowType.RewardWindow;
        
        public override void Initialize(TaskService taskService, CurrencyManager currencyManager)
        {
            base.Initialize(taskService, currencyManager);
            _animationController = new RewardWindowAnimationController(
                rewardWindowSettings,
                cardRoot,
                cardCanvasGroup,
                cardFront,
                cardBack
            );
        }
        
        public override void OnWindowActivated(object customData)
        {
            base.OnWindowActivated(customData);
            _rewardWindowCustomInfo = customData as RewardWindowCustomInfo;

            if (_rewardWindowCustomInfo == null)
            {
                Debug.LogError("[RewardWindow] Custom data is null!");
                return;
            }
            
            UpdateRewardInfo(_rewardWindowCustomInfo.RewardData, _rewardWindowCustomInfo.RewardAmount);
            _animationController.PlayCardAnimation()
                .OnComplete(() => CloseWindowWithTask(WindowType.RewardWindow, TaskType.ShowRewardWindow));
        }

        public override void OnWindowDeactivated()
        {
            DOTween.Kill(transform);
            base.OnWindowDeactivated();
            _animationController.ResetCardRotation();
            cardCanvasGroup.alpha = 1;
        }

        private void UpdateRewardInfo(RewardData rewardData, int rewardAmount)
        {
            rewardIcon.sprite = rewardData.RewardInfo.Icon;
            rewardTitleText.text = rewardData.RewardInfo.Title;
            rewardAmountText.text = $"x{rewardAmount}";
        }
    }
}
