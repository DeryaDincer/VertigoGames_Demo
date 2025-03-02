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

        public void SetWheelVisual(WheelZoneAppearance wheelZoneAppearance)
        {
            _spinWheelImageValue.sprite = wheelZoneAppearance.WheelBaseSprite;
            _indicatorWheelImageValue.sprite = wheelZoneAppearance.WheelIndicatorSprite;
        }
    }

}
