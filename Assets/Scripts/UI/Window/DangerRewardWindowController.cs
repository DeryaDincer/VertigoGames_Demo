using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.Managers;
using VertigoGames.Services;
using VertigoGames.Settings;

namespace VertigoGames.UI.Window
{
    public class DangerRewardWindowController : Window
    {
         [Header("UI References")]
        [SerializeField] private Image _rewardIcon;
        [SerializeField] private RectTransform _cardRoot;
        [SerializeField] private Image _windowBackgroundImage;
        [SerializeField] private TextMeshProUGUI _dangerInfoTextValue;
            
        [Header("Danger Reward Window Settings")]
        [SerializeField] private DangerRewardWindowSettings _dangerRewardWindowSettings;

        private RewardWindowCustomInfo _rewardWindowCustomInfo;
        private DangerRewardWindowAnimationController _animationController;

        public override WindowType WindowType => WindowType.DangerRewardWindow;

        private void Awake()
        {
            _animationController = new DangerRewardWindowAnimationController(
                _dangerRewardWindowSettings,
                _cardRoot,
                _windowBackgroundImage);
        }

        public override void OnWindowActivated(object customData)
        {
            base.OnWindowActivated(customData);
            _rewardWindowCustomInfo = customData as RewardWindowCustomInfo;

            if (_rewardWindowCustomInfo == null)
            {
                Debug.LogError("DangerRewardWindowCustomData is null!");
                return;
            }

            SetRewardInfo(_rewardWindowCustomInfo.RewardData);
            _animationController.PlayCardAnimation().OnComplete(CloseWindow);
        }

        public override void OnWindowDeactivated()
        {
            DOTween.Kill(transform);
            base.OnWindowDeactivated();
            _animationController.ResetCardRotation();
        }

        private void SetRewardInfo(RewardData rewardData)
        {
            _rewardIcon.sprite = rewardData.RewardInfo.Icon;
        }

        private void CloseWindow()
        {
            WindowStateChangeInfo windowStateChangeInfo = new WindowStateChangeInfo
            {
                WindowType = WindowType.DangerRewardWindow,
                ActiveStatus = false,
                CustomInfo = null
            };

            ObserverManager.Notify(new WindowStateChangeEvent(windowStateChangeInfo));
            TaskService.Instance.CompleteTask(TaskType.ShowRewardWindow);
        }
    }
}

