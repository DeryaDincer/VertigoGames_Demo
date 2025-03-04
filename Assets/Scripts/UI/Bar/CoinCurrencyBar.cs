using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VertigoGames.Events;
using VertigoGames.Managers;

namespace VertigoGames.UI.Bar
{
    public class CoinCurrencyBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinCurrencyTextValue;
        
        private void OnEnable()
        {
            SetGoldText();
            ObserverManager.Register<OnCurrencyChangedEvent>(OnCurrencyChanged);
        }
        
        private void OnDisable()
        {
            ObserverManager.Unregister<OnCurrencyChangedEvent>(OnCurrencyChanged);
        }

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
            _coinCurrencyTextValue.text = CurrencyManager.Instance.GetCurrencyAmount(RewardType.Gold).ToString();
        }
    }
}