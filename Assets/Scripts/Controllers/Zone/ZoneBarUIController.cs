using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Settings;

namespace VertigoGames.Controllers.Zone
{
    public class ZoneBarUIController
    {
        private readonly List<TextMeshProUGUI> _indicatorTexts = new();
        private readonly RectTransform _layoutGroupRect;
        private readonly Image _indicatorImageValue;
        private readonly ZoneBarSettings _settings;

        public ZoneBarUIController(ZoneBarSettings settings, RectTransform layoutGroupRect, Image indicatorImageValue)
        {
            _settings = settings;
            _layoutGroupRect = layoutGroupRect;
            _indicatorImageValue = indicatorImageValue;
        }

        public void Initialize(TextMeshProUGUI indicatorTextPrefab)
        {
            CalculateSlideDistance();
            InitializeZoneIndicators(indicatorTextPrefab);
            SetInitialPosition();
        }

        public void ResetUI()
        {
            for (int i = 0; i < _indicatorTexts.Count; i++)
            {
                _indicatorTexts[i].text = (i + 1).ToString();
            }
            
            CalculateSlideDistance();
            SetZoneUI(_settings.GetInitialZoneType());
        }

        public void UpdateIndicators()
        {
            foreach (var indicatorText in _indicatorTexts)
            {
                int currentValue = int.Parse(indicatorText.text);
                indicatorText.text = (currentValue + 1).ToString();
            }
        }

        public void SetZoneUI(ZoneType zoneType)
        {
            ZoneBarAppearanceInfo zoneBarAppearanceInfo = _settings.GetZoneBarAppearanceByZoneType(zoneType);
            _indicatorImageValue.sprite = zoneBarAppearanceInfo.ZoneBaseSprite;
        }

        private void CalculateSlideDistance()
        {
            float initialPositionX = _settings.SlideDistance * _settings.AverageIndicatorIndex;
            _layoutGroupRect.anchoredPosition = new Vector2(initialPositionX, 0);
        }

        private void InitializeZoneIndicators(TextMeshProUGUI indicatorTextPrefab)
        {
            for (int i = 0; i < _settings.InitialIndicatorCount; i++)
            {
                TextMeshProUGUI indicatorText = Object.Instantiate(indicatorTextPrefab, _layoutGroupRect);
                indicatorText.text = i.ToString();
                _indicatorTexts.Add(indicatorText);
            }
        }

        private void SetInitialPosition()
        {
            float initialPositionX = _settings.SlideDistance * _settings.AverageIndicatorIndex;
            _layoutGroupRect.anchoredPosition = new Vector2(initialPositionX, 0);
        }
    }
}