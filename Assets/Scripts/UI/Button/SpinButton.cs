using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Events;
using VertigoGames.Managers;

namespace VertigoGames.UI.Button
{
    public class SpinButton : MonoBehaviour
    {
        private UnityEngine.UI.Button button => GetComponent<UnityEngine.UI.Button>();
        private WheelController _wheelController;
        private void OnValidate()
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(ClickButton);
        }

        public void SetWheelController(WheelController wheelController)
        {
            _wheelController = wheelController;
        }

        private void ClickButton()
        {
            _wheelController.OnSpinWheel();
            // ObserverManager.Notify(new SpinWheelEvent());
        }
    }
}