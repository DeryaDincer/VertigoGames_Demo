using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.Managers;
using VertigoGames.Services;
using VertigoGames.Settings;

namespace VertigoGames.UI.Window
{
    public class DeadZoneWindowController : Window
    {
        [Header("UI References")]
        [SerializeField] private Image _rewardIcon;
        [SerializeField] private RectTransform _cardRoot;
        [SerializeField] private Image _windowBackgroundImage;
        [SerializeField] private TextMeshProUGUI _deadZoneInfoTextValue;
            
        [Header("Dead Zone Window Settings")]
        [SerializeField] private DeadZoneWindowSettings deadZoneWindowSettings;

        private DangerZoneWindowCustomInfo _dangerZoneWindowCustomInfo;
        private DeadZoneWindowAnimationController _animationController;
        private TaskService _taskService;
        public override WindowType WindowType => WindowType.DeadZoneWindow;

        public override void Initialize(TaskService taskService)
        {
            base.Initialize(taskService);
            _taskService = taskService;
            
            _animationController = new DeadZoneWindowAnimationController(
                deadZoneWindowSettings,
                _cardRoot,
                _windowBackgroundImage);
        }

        public override void Register()
        {
            base.Register();
            ObserverManager.Register<GameSessionReviveEvent>(GameSessionRevive);
            ObserverManager.Register<GameSessionResetEvent>(GameSessionReset);

        }

        public override void Unregister()
        {
            base.Unregister();
            ObserverManager.Unregister<GameSessionReviveEvent>(GameSessionRevive);
            ObserverManager.Unregister<GameSessionResetEvent>(GameSessionReset);
        }
        
        public override void OnWindowActivated(object customData)
        {
            base.OnWindowActivated(customData);
            _dangerZoneWindowCustomInfo = customData as DangerZoneWindowCustomInfo;

            if (_dangerZoneWindowCustomInfo == null)
            {
                Debug.LogError("DaeadZoneWindowCustomData is null!");
                return;
            }

            SetRewardInfo(_dangerZoneWindowCustomInfo.RewardData);
            _animationController.PlayCardAnimation();
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

        private void GameSessionRevive(GameSessionReviveEvent obj) => CloseWindow();

        private void GameSessionReset(GameSessionResetEvent obj) => CloseWindow();

        private void CloseWindow()
        {
            WindowStateChangeInfo windowStateChangeInfo = new WindowStateChangeInfo
            {
                WindowType = WindowType.DeadZoneWindow,
                ActiveStatus = false,
                CustomInfo = null
            };

            ObserverManager.Notify(new WindowStateChangeEvent(windowStateChangeInfo));
            _taskService.CompleteTask(TaskType.ShowDeadZoneWindow);
        }
    }
}

