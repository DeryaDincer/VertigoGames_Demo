using UnityEngine;
using VertigoGames.Controllers.Wheel;

namespace VertigoGames.UI.Button
{
    public class SpinButton : MonoBehaviour
    {
        private UnityEngine.UI.Button _button => GetComponent<UnityEngine.UI.Button>();
        private WheelController _wheelController;
        private void OnValidate()
        {
            Debug.LogError("OnValidate");
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(ClickButton);
        }

        
        private void OnEnable()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(ClickButton);
        }

        public void SetWheelController(WheelController wheelController)
        {
            _wheelController = wheelController;
        }

        private void ClickButton()
        {
            _wheelController.SpinWheel();
            // ObserverManager.Notify(new SpinWheelEvent()); event kullan
        }
    }
}