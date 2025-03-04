using UnityEngine;
using VertigoGames.Events;
using VertigoGames.Managers;

namespace VertigoGames.UI.Button
{
    public class ReviveGameButton : MonoBehaviour
    {
        private UnityEngine.UI.Button button => GetComponent<UnityEngine.UI.Button>();
        private void OnValidate()
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(ClickButton);
        }

        private void ClickButton()
        {
            ObserverManager.Notify(new GameSessionReviveEvent());
        }
    }
}