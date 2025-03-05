using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Events;
using VertigoGames.GameTasks;
using VertigoGames.Interfaces;
using VertigoGames.Managers;
using VertigoGames.Services;
using VertigoGames.Settings;

namespace VertigoGames.Controllers.Zone
{
    public class ZoneBarController : MonoBehaviour, IRegisterable
    {
        [Title("Settings References")]
        [SerializeField] private ZoneBarSettings zoneBarSettings;

        [Title("UI References")]
        [SerializeField] private TextMeshProUGUI indicatorTextPrefab;
        [SerializeField] private RectTransform layoutGroupRect;
        [SerializeField] private Image indicatorImageValue;

        private ZoneBarAnimationController _animationController;
        private ZoneBarUIController _uiController;
        private ITaskService _taskService;

        #region Initialization and Deinitialization
        public void Initialize(ITaskService taskService)
        {
            _taskService = taskService;

            _uiController = new ZoneBarUIController(zoneBarSettings, layoutGroupRect, indicatorImageValue);
            _uiController.Initialize(indicatorTextPrefab);

            _animationController = new ZoneBarAnimationController(layoutGroupRect, zoneBarSettings);
        }
        #endregion

        #region Registration and Unregistration
        public void Register() => ObserverManager.Register<RewardDeterminedEvent>(OnRewardDetermined);
        public void Unregister() => ObserverManager.Unregister<RewardDeterminedEvent>(OnRewardDetermined);
        #endregion

        public void BeginGameSession()
        {
            _uiController.ResetUI();
            _animationController.ResetBarData();
        }

        private void OnRewardDetermined(RewardDeterminedEvent obj)
        {
            var zoneBarTask = new ZoneBarTask(async () =>
            {
                SlideToNextZone();
                _uiController.SetZoneUI(obj.ZoneData.ZoneType);
            });

            _taskService.AddTask(zoneBarTask);
        }

        private void SlideToNextZone()
        {
            _animationController.SlideToNextZone((bool shouldResetPosition) =>
            {
                if (shouldResetPosition)
                {
                    _uiController.UpdateIndicators();
                }

                _taskService.CompleteTask(TaskType.ZoneBar);
            });
        }
    }
}