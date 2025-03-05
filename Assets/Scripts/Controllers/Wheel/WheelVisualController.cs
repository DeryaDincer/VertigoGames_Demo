using UnityEngine.UI;

namespace VertigoGames.Controllers.Wheel
{
    public class WheelVisualController 
    {
        private readonly Image _spinWheelImageValue;
        private readonly Image _indicatorWheelImageValue;

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
