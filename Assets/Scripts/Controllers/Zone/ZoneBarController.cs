using System.Collections.Generic;
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
        [Header("Settings References")]
        [SerializeField] private ZoneBarSettings zoneBarSettings;

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI indicatorTextPrefab;
        [SerializeField] private RectTransform layoutGroupRect;
        [SerializeField] private Image indicatorImageValue;

        private readonly List<ZoneBarIndicatorInfo> _zoneIndicators = new();
        private float _initialPositionX;

        private ZoneBarAnimationController _animationController;
        private ITaskService _taskService;
        
        #region Initialization and Deinitialization
        public void Initialize(ITaskService taskService)
        {
            _taskService = taskService;
            
            CalculateSlideDistance();
            InitializeZoneIndicators();
            SetInitialPosition();

            _animationController = new ZoneBarAnimationController(layoutGroupRect, zoneBarSettings);
        }
        #endregion

        #region Registration and Unregistration
        public void Register() =>  ObserverManager.Register<RewardDeterminedEvent>(OnRewardDetermined);
        public void Unregister() => ObserverManager.Unregister<RewardDeterminedEvent>(OnRewardDetermined);
        #endregion
        
        public void BeginGameSession()
        {
            SetInitialPosition();
            ResetZoneIndicators();
        }

        private void CalculateSlideDistance()
        {
            _initialPositionX = zoneBarSettings.SlideDistance * zoneBarSettings.AverageIndicatorIndex;
        }

        private void InitializeZoneIndicators()
        {
            for (int i = 0; i < zoneBarSettings.InitialIndicatorCount; i++)
            {
                TextMeshProUGUI indicatorText = Instantiate(indicatorTextPrefab, layoutGroupRect.transform);
                indicatorText.text = i.ToString();
                _zoneIndicators.Add(new ZoneBarIndicatorInfo(indicatorText, i));
            }
        }

        private void SetInitialPosition()
        {
            layoutGroupRect.anchoredPosition = new Vector2(_initialPositionX, 0);
        }

        private void ResetZoneIndicators()
        {
            for (int i = 0; i < _zoneIndicators.Count; i++)
            {
                _zoneIndicators[i].Value = i + 1;
                _zoneIndicators[i].Text.text = _zoneIndicators[i].Value.ToString();
            }

            SetZoneUI(zoneBarSettings.GetInitialZoneType());
            _animationController.ResetBarData();
        }

        private void OnRewardDetermined(RewardDeterminedEvent obj)
        {
            var zoneBarTask = new ZoneBarTask(async () =>
            {
                SlideToNextZone();
                SetZoneUI(obj.ZoneData.ZoneType);
            });

            _taskService.AddTask(zoneBarTask);
        }

        private void SlideToNextZone()
        {
            _animationController.SlideToNextZone((bool shouldResetPosition) =>
            {
                if (shouldResetPosition)
                {
                    UpdateZoneIndicators();
                }

                _taskService.CompleteTask(TaskType.ZoneBar);
            });
        }

        private void SetZoneUI(ZoneType zoneType)
        {
            ZoneBarAppearanceInfo zoneBarAppearanceInfo = zoneBarSettings.GetZoneBarAppearanceByZoneType(zoneType);
            indicatorImageValue.sprite = zoneBarAppearanceInfo.ZoneBaseSprite;
        }

        private void UpdateZoneIndicators()
        {
            foreach (var indicator in _zoneIndicators)
            {
                indicator.Value++;
                indicator.Text.text = indicator.Value.ToString();
            }
        }
    }
} 