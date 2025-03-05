using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.Managers;
using VertigoGames.Services;
using VertigoGames.Settings;
using VertigoGames.UI.Button;
using VertigoGames.Utility;

namespace VertigoGames.UI.Window
{
    public class DeadZoneWindowController : BaseWindowController
    {
        [Title("UI References")]
        [SerializeField] private Image rewardIcon;
        [SerializeField] private RectTransform cardRoot;
        [SerializeField] private Image windowBackgroundImage;

        [Title("Dead Zone Window Settings")]
        [SerializeField] private DeadZoneWindowSettings deadZoneWindowSettings;

        [Title("Dead Zone Button")]
        [SerializeField] private ReviveGameButton reviveGameButton;

        private DeadZoneWindowCustomInfo _deadZoneWindowCustomInfo;
        private DeadZoneWindowAnimationController _animationController;

        public override WindowType WindowType => WindowType.DeadZoneWindow;

        public override void Initialize(TaskService taskService, CurrencyManager currencyManager)
        {
            base.Initialize(taskService, currencyManager);
            reviveGameButton.Initialize(currencyManager, deadZoneWindowSettings.ReviveGoldAmount);
            _animationController = new DeadZoneWindowAnimationController(deadZoneWindowSettings, cardRoot, windowBackgroundImage);
        }

        #region IRegisterable Implementation
        public override void Register()
        {
            base.Register();
            ObserverManager.Register<GameSessionRevivedEvent>(OnGameSessionRevived);
            ObserverManager.Register<GameSessionResetEvent>(OnGameSessionReset);
        }

        public override void Unregister()
        {
            base.Unregister();
            ObserverManager.Unregister<GameSessionRevivedEvent>(OnGameSessionRevived);
            ObserverManager.Unregister<GameSessionResetEvent>(OnGameSessionReset);
        }
        #endregion

        public override void OnWindowActivated(object customData)
        {
            base.OnWindowActivated(customData);
            _deadZoneWindowCustomInfo = customData as DeadZoneWindowCustomInfo;

            if (_deadZoneWindowCustomInfo == null)
            {
                Debug.LogError("[DeadZoneWindow] Custom data is null!");
                return;
            }

            UpdateRewardInfo(_deadZoneWindowCustomInfo.RewardData);
            UpdateReviveButton();
            _animationController.PlayCardAnimation();
        }

        public override void OnWindowDeactivated()
        {
            DOTween.Kill(transform);
            base.OnWindowDeactivated();
            _animationController.ResetCardRotation();
        }

        private void UpdateRewardInfo(RewardData rewardData)
        {
            rewardIcon.sprite = rewardData.RewardInfo.Icon;
        }

        private void UpdateReviveButton()
        {
            int reviveCost = deadZoneWindowSettings.ReviveGoldAmount;
            bool hasEnoughGold = CurrencyManager.GetCurrencyAmount(RewardType.Gold) >= reviveCost;

            reviveGameButton.SetReviveGoldAmount(reviveCost, deadZoneWindowSettings.TextGoldSpriteString);

            if (hasEnoughGold)
                reviveGameButton.Show();
            else
                reviveGameButton.Hide();
        }

        private void OnGameSessionRevived(GameSessionRevivedEvent eventData) => CloseWindow();

        private void OnGameSessionReset(GameSessionResetEvent eventData) => CloseWindow();

        private void CloseWindow()
        {
            CloseWindowWithTask(WindowType.DeadZoneWindow, TaskType.ShowDeadZoneWindow);
        }
    }
}
