using UnityEngine;

namespace VertigoGames.UI.Button
{
    public abstract class BaseButton : MonoBehaviour
    {
        private UnityEngine.UI.Button _button;

        protected virtual void Awake() => _button = GetComponent<UnityEngine.UI.Button>();

        protected virtual void OnEnable() => _button.onClick.AddListener(OnButtonClicked);

        protected virtual void OnDisable() => _button.onClick.RemoveAllListeners();

        protected abstract void OnButtonClicked();
    }
}