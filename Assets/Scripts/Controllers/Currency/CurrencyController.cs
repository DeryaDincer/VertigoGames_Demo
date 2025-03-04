using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VertigoGames.Events;
using VertigoGames.Managers;

namespace VertigoGames.Controllers.Currency
{
    public class CurrencyController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinCurrencyTextValue;
        private CurrencyManager _currencyManager;
        
        #region Initialization and Deinitialization

        public void Initialize(CurrencyManager currencyManager)
        {
            _currencyManager = currencyManager;
            SetGoldText();
        }

        public void Deinitialize()
        {
        }

        #endregion

        #region Registration and Unregistration

        public void Register()
        {
            ObserverManager.Register<OnCurrencyChangedEvent>(OnCurrencyChanged);
        }

        public void Unregister()
        {
            ObserverManager.Unregister<OnCurrencyChangedEvent>(OnCurrencyChanged);

        }

        #endregion
        
        private void OnCurrencyChanged(OnCurrencyChangedEvent obj)
        {
            if (obj.RewardType != RewardType.Gold)
            {
                return;
            }

            SetGoldText();
        }

        private void SetGoldText()
        {
            _coinCurrencyTextValue.text = _currencyManager.GetCurrencyAmount(RewardType.Gold).ToString();
        }
    }
}