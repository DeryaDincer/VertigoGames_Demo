using TMPro;
using UnityEngine;
using VertigoGames.Events;
using VertigoGames.Managers;

namespace VertigoGames.UI.Button
{
    public class ReviveGameButton : BaseButton
    {
        [SerializeField] private TextMeshProUGUI _reviveButtonText;
        private CurrencyManager _currencyManager;
        private int _goldAmount;
        private readonly string _buttonString = "\nREVIVE";
        
        protected override void OnButtonClicked()
        {
            ObserverManager.Notify(new GameSessionRevivedEvent());
            _currencyManager.ModifyCurrency(RewardType.Gold, -_goldAmount);
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
    }
}