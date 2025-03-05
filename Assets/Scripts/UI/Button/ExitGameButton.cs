using UnityEngine;
using VertigoGames.Events;
using VertigoGames.Managers;

namespace VertigoGames.UI.Button
{
    public class ExitGameButton : MonoBehaviour
    {
        private UnityEngine.UI.Button _button => GetComponent<UnityEngine.UI.Button>();
        private void OnValidate()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(ClickButton);
        }

        private void ClickButton()
        {
             ObserverManager.Notify(new GameSessionResetEvent());
        }
    }
}
