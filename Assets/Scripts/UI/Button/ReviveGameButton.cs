using TMPro;
using UnityEngine;
using VertigoGames.Events;
using VertigoGames.Managers;

namespace VertigoGames.UI.Button
{
    public class ReviveGameButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _reviveButtonText;
        private UnityEngine.UI.Button _button => GetComponent<UnityEngine.UI.Button>();
        private CurrencyManager _currencyManager;
        private int _goldAmount;
        private readonly string _buttonString = "\nREVIVE";
        
        private void OnValidate()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(ClickButton);
        }

        public void Initialize(CurrencyManager currencyManager, int goldAmount)
        {
            _currencyManager = currencyManager;
            _goldAmount = goldAmount;
        }

        public void SetReviveGoldAmount(int amount, string textString)
        {
            _reviveButtonText.text =  textString + amount + _buttonString;
        }
        
        private void ClickButton()
        {
            ObserverManager.Notify(new GameSessionRevivedEvent());
            _currencyManager.ModifyCurrency(RewardType.Gold, -_goldAmount);
        }
    }
}