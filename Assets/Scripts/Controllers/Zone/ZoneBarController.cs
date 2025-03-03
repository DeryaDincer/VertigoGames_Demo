using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Managers;
using VertigoGames.Settings;
using VertigoGames.TaskService;

namespace VertigoGames.Controllers.Zone
{
    public class ZoneBarController : MonoBehaviour, IInitializable, IRegisterable
    {
        [Header("Settings References")] 
        [SerializeField] private ZoneBarSettings _zoneBarSettings;
        
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI _indicatorTextPrefab; 
        [SerializeField] private RectTransform _layoutGroupRect; 

        private readonly List<ZoneIndicator> _zoneIndicators = new();

        private int _initialIndicatorCount; 
        private float _slideDuration = 1f; 
        private int _averageIndicatorIndex;
        private float _slideDistance;
        private float _initialPositionX;
        private Ease _slideEase;
        private int _slideCount = 0;

        public void Initialize()
        {
            _initialIndicatorCount = _zoneBarSettings.InitialIndicatorCount;
            _slideDuration = _zoneBarSettings.SlideDuration;
            _slideEase = _zoneBarSettings.SlideEase;
            _averageIndicatorIndex = _zoneBarSettings.AverageIndicatorIndex;
            _slideDistance = _zoneBarSettings.SlideDistance;
            
            CalculateSlideDistance();
            InitializeZoneIndicators();
            SetInitialPosition();
        }

        public void Deinitialize()
        {
           
        }
        
        public void Register()
        {
            ObserverManager.Register<OnRewardDetermined>(OnRewardDetermined);
        }

        public void Unregister()
        {
            ObserverManager.Unregister<OnRewardDetermined>(OnRewardDetermined);
        }

        public void BeginGameSession()
        {
            SetInitialPosition();
            for (int i = 0; i < _zoneIndicators.Count; i++)
            {
                _zoneIndicators[i].Value = i + 1;
                _zoneIndicators[i].Text.text = _zoneIndicators[i].Value.ToString();
            }
        }
        
        private void CalculateSlideDistance()
        {
            _initialPositionX = _slideDistance * _averageIndicatorIndex;
        }

        private void InitializeZoneIndicators()
        {
            for (int i = 0; i < _initialIndicatorCount; i++)
            {
                TextMeshProUGUI indicatorText = Instantiate(_indicatorTextPrefab, _layoutGroupRect.transform);
                indicatorText.text = i.ToString();
                _zoneIndicators.Add(new ZoneIndicator(indicatorText, i));
            }
        }

        private void SetInitialPosition()
        {
            _layoutGroupRect.anchoredPosition = new Vector2(_initialPositionX, 0);
        }

        private void OnRewardDetermined(OnRewardDetermined obj)
        {
            var zoneBarTask = new ZoneBarTask(async () =>
            {
                SlideToNextZone();
            });
            
            TaskService.TaskService.Instance.AddTask(zoneBarTask);
        }
        
        private void SlideToNextZone()
        {
            float currentPositionX = _layoutGroupRect.anchoredPosition.x;
            Sequence slideSequence = DOTween.Sequence();

            slideSequence.Append(_layoutGroupRect.DOAnchorPosX(currentPositionX - _slideDistance, _slideDuration)
                         .SetEase(_slideEase));

            slideSequence.AppendCallback(() =>
            {
                if (_slideCount >= _averageIndicatorIndex)
                {
                    UpdateZoneIndicators();
                    ResetPosition();
                }

                _slideCount++;
            });

            slideSequence.AppendCallback(() =>
            {
                TaskService.TaskService.Instance.CompleteTask(TaskType.ZoneBar);
            });
        }

        private void UpdateZoneIndicators()
        {
            foreach (var indicator in _zoneIndicators)
            {
                indicator.Value++;
                indicator.Text.text = indicator.Value.ToString();
            }
        }

        private void ResetPosition()
        {
            _layoutGroupRect.anchoredPosition = Vector2.zero;
        }
    }

    public class ZoneIndicator
    {
        public TextMeshProUGUI Text { get; private set; }
        public int Value { get; set; }

        public ZoneIndicator(TextMeshProUGUI text, int value)
        {
            Text = text;
            Value = value;
        }
    }
}