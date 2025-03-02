using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Events;
using VertigoGames.Managers;

namespace VertigoGames.UI
{
    public class SpinButton : MonoBehaviour
    {
        private Button button => GetComponent<Button>();
        
        private void OnValidate()
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(ClickButton);
        }

        private void ClickButton()
        {
            ObserverManager.Notify(new SpinWheelEvent());
        }
    }
}