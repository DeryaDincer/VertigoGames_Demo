using UnityEngine;
using VertigoGames.Controllers.Currency;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Utility;

namespace VertigoGames.Managers
{
    public class CurrencyManager : MonoBehaviour, IRegisterable
    {
        [SerializeField] private CurrencyController currencyController;
        
        #region Initialization and Deinitialization
        public void Initialize() => currencyController.Initialize(this);

        public void Deinitialize() => currencyController.Deinitialize();
        #endregion

        #region Registration and Unregistration
        public void Register() =>  currencyController.Register();

        public void Unregister() => currencyController.Unregister();
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

