using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Events;
using VertigoGames.Managers;

namespace VertigoGames.UI.Button
{
    public class ExitGameButton : MonoBehaviour
    {
        private UnityEngine.UI.Button button => GetComponent<UnityEngine.UI.Button>();
        private void OnValidate()
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(ClickButton);
        }

        private void ClickButton()
        {
            Debug.LogError("reset game");
             ObserverManager.Notify(new OnResetGameEvent());
        }
    }
}
