using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Settings;

namespace VertigoGames.Controllers.Wheel
{
    public class WheelVisualController 
    {
        private Image _spinWheelImageValue;
        private Image _indicatorWheelImageValue;

        public WheelVisualController(Image spinWheelImageValue, Image indicatorWheelImageValue)
        {
            _spinWheelImageValue = spinWheelImageValue;
            _indicatorWheelImageValue = indicatorWheelImageValue;
        }

        public void SetWheelVisual(WheelZoneAppearanceInfo wheelZoneAppearanceInfo)
        {
            _spinWheelImageValue.sprite = wheelZoneAppearanceInfo.WheelBaseSprite;
            _indicatorWheelImageValue.sprite = wheelZoneAppearanceInfo.WheelIndicatorSprite;
        }
    }

}
