using System.Collections.Generic;
using DG.Tweening;
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
    /// <summary>
    /// Controls the behavior of the zone bar, including initialization, animation, and updating indicators.
    /// </summary>
    public class ZoneBarController : MonoBehaviour, IInitializable, IRegisterable
    {
        [Header("Settings References")]
        [SerializeField] private ZoneBarSettings _zoneBarSettings;

        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI _indicatorTextPrefab;
        [SerializeField] private RectTransform _layoutGroupRect;
        [SerializeField] private Image _indicatorImageValue;

        private readonly List<ZoneBarIndicatorInfo> _zoneIndicators = new();
        // private int _initialIndicatorCount;
        //private int _averageIndicatorIndex;
        // private float _slideDistance;
        private float _initialPositionX;
        private int _slideCount = 0;

        private ZoneBarAnimationController _animationController;

        #region Initialization and Deinitialization

        public void Initialize()
        {
            CalculateSlideDistance();
            InitializeZoneIndicators();
            SetInitialPosition();

            _animationController = new ZoneBarAnimationController(_layoutGroupRect, _zoneBarSettings);
        }

        public void Deinitialize()
        {
            
        }

        #endregion

        #region Registration and Unregistration
        
        public void Register() =>  ObserverManager.Register<OnRewardDetermined>(OnRewardDetermined);

        public void Unregister() => ObserverManager.Unregister<OnRewardDetermined>(OnRewardDetermined);
        
        #endregion
        
        public void BeginGameSession()
        {
            SetInitialPosition();
            ResetZoneIndicators();
        }

        private void CalculateSlideDistance()
        {
            _initialPositionX = _zoneBarSettings.SlideDistance * _zoneBarSettings.AverageIndicatorIndex;
        }

        private void InitializeZoneIndicators()
        {
            for (int i = 0; i < _zoneBarSettings.InitialIndicatorCount; i++)
            {
                TextMeshProUGUI indicatorText = Instantiate(_indicatorTextPrefab, _layoutGroupRect.transform);
                indicatorText.text = i.ToString();
                _zoneIndicators.Add(new ZoneBarIndicatorInfo(indicatorText, i));
            }
        }

        private void SetInitialPosition()
        {
            _layoutGroupRect.anchoredPosition = new Vector2(_initialPositionX, 0);
        }

        private void ResetZoneIndicators()
        {
            for (int i = 0; i < _zoneIndicators.Count; i++)
            {
                _zoneIndicators[i].Value = i + 1;
                _zoneIndicators[i].Text.text = _zoneIndicators[i].Value.ToString();
            }
        }

        private void OnRewardDetermined(OnRewardDetermined obj)
        {
            var zoneBarTask = new ZoneBarTask(async () =>
            {
                SlideToNextZone();
                SetZoneUI(obj.ZoneData.ZoneType);
            });

            TaskService.Instance.AddTask(zoneBarTask);
        }

        private void SlideToNextZone()
        {
            _animationController.SlideToNextZone(() =>
            {
                if (_slideCount >= _zoneBarSettings.AverageIndicatorIndex)
                {
                    UpdateZoneIndicators();
                    _animationController.ResetPosition();
                }

                _slideCount++;

                TaskService.Instance.CompleteTask(TaskType.ZoneBar);
            });
        }

        private void SetZoneUI(ZoneType zoneType)
        {
            ZoneBarAppearanceInfo zoneBarAppearanceInfo = _zoneBarSettings.GetZoneBarAppearanceByZoneType(zoneType);
            _indicatorImageValue.sprite = zoneBarAppearanceInfo.ZoneBaseSprite;
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