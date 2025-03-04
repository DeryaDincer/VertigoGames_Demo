using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.Managers;
using VertigoGames.Services;
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

        private RewardWindowCustomInfo _rewardWindowCustomInfo;
        private RewardWindowAnimationController _animationController;
        private TaskService _taskService;
        public override WindowType WindowType => WindowType.RewardWindow;

        public override void Initialize(TaskService taskService)
        {
            base.Initialize(taskService);
            _taskService = taskService;
            
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
            _rewardWindowCustomInfo = customData as RewardWindowCustomInfo;

            if (_rewardWindowCustomInfo == null)
            {
                Debug.LogError("RewardWindowCustomData is null!");
                return;
            }
          
            SetRewardInfo(_rewardWindowCustomInfo.RewardData, _rewardWindowCustomInfo.RewardAmount);
            _animationController.PlayCardAnimation().OnComplete(CloseWindow);
        }

        public override void OnWindowDeactivated()
        {
            DOTween.Kill(transform);
            base.OnWindowDeactivated();
            _animationController.ResetCardRotation();
            _cardCanvasGroup.alpha = 1;
        }

        private void SetRewardInfo(RewardData rewardData, int rewardAmount)
        {
            _rewardIcon.sprite = rewardData.RewardInfo.Icon;
            _rewardTitleValue.text = rewardData.RewardInfo.Title;
            _rewardAmountValue.text = "x" + rewardAmount; 
        }

        private void CloseWindow()
        {
            WindowStateChangeInfo windowStateChangeInfo = new WindowStateChangeInfo
            {
                WindowType = WindowType.RewardWindow,
                ActiveStatus = false,
                CustomInfo = null
            };

            ObserverManager.Notify(new WindowStateChangeEvent(windowStateChangeInfo));
            _taskService.CompleteTask(TaskType.ShowRewardWindow);
        }
    }
}

