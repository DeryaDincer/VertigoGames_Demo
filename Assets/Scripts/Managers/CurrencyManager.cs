using UnityEngine;
using VertigoGames.Controllers.Currency;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Settings;
using VertigoGames.Utility;

namespace VertigoGames.Managers
{
    public class CurrencyManager : MonoBehaviour, IRegisterable
    {
        private CurrencyController _currencyController;
        
        #region Initialization and Deinitialization
        public void Initialize(GamePrefabSettings gamePrefabSettings)
        {
            ModifyCurrency(RewardType.Gold, 100);
            _currencyController = Instantiate(gamePrefabSettings.CurrencyController);
            _currencyController.Initialize(this);
        }

        public void Deinitialize() => _currencyController.Deinitialize();
        #endregion

        #region Registration and Unregistration
        public void Register() =>  _currencyController.Register();

        public void Unregister() => _currencyController.Unregister();
        #endregion

        public void ModifyCurrency(RewardType rewardType, int amount)
        {
            int updatedAmount = LoadCurrency(rewardType) + amount;
            SaveCurrency(rewardType, updatedAmount);
            ObserverManager.Notify(new CurrencyChangedEvent(rewardType));
        }
        
        private int LoadCurrency(RewardType rewardType)
        {
            string currencyKey = GetCurrencyKey(rewardType);
            return DataSaver.GetData<int>(currencyKey, 0);
        }

        private void SaveCurrency(RewardType rewardType, int amount)
        {
            string currencyKey = GetCurrencyKey(rewardType);
            DataSaver.SetData(currencyKey, amount);
        }
        
        public int GetCurrencyAmount(RewardType rewardType)
        {
            int currentAmount = LoadCurrency(rewardType);
            return currentAmount;
        }

        private string GetCurrencyKey(RewardType rewardType) => $"Currency_{rewardType}";
    }
}

