using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using VertigoGames.Controllers.Currency;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Utility;

namespace VertigoGames.Managers
{
    public class CurrencyManager : MonoBehaviour, IRegisterable
    {
        [SerializeField] private CurrencyController _currencyController;
        
        #region Initialization and Deinitialization

        public void Initialize()
        {
            _currencyController.Initialize(this);
        }

        public void Deinitialize()
        {
            _currencyController.Deinitialize();
        }

        #endregion

        #region Registration and Unregistration

        public void Register()
        {
            _currencyController.Register();
        }

        public void Unregister()
        {
            _currencyController.Unregister();
        }

        #endregion

        public int LoadCurrency(RewardType rewardType)
        {
            string currencyKey = GetCurrencyKey(rewardType);
            return DataSaver.GetData<int>(currencyKey, 0);
        }

        private void SaveCurrency(RewardType rewardType, int amount)
        {
            string currencyKey = GetCurrencyKey(rewardType);
            DataSaver.SetData(currencyKey, amount);
        }

        public void AddCurrency(RewardType rewardType, int amount)
        {
            int currentAmount = LoadCurrency(rewardType);
            currentAmount += amount;
            SaveCurrency(rewardType, currentAmount);
            ObserverManager.Notify(new OnCurrencyChangedEvent(rewardType));
        }
        
        public int GetCurrencyAmount(RewardType rewardType)
        {
            int currentAmount = LoadCurrency(rewardType);
            return currentAmount;
        }

        public void DecreaseCurrency(RewardType rewardType, int amount)
        {
            int currentAmount = LoadCurrency(rewardType);
            currentAmount = Mathf.Max(0, currentAmount - amount);
            SaveCurrency(rewardType, currentAmount);
            ObserverManager.Notify(new OnCurrencyChangedEvent(rewardType));
        }

        public int GetCurrency(RewardType rewardType)
        {
            return LoadCurrency(rewardType);
        }

        public void ResetCurrency(RewardType rewardType)
        {
            SaveCurrency(rewardType, 0);
            ObserverManager.Notify(new OnCurrencyChangedEvent(rewardType));
        }

        private string GetCurrencyKey(RewardType rewardType)
        {
            return $"Currency_{rewardType}";
        }
    }
}

