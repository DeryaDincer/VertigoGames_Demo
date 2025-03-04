using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using VertigoGames.Controllers;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Controllers.Zone;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Services;

namespace VertigoGames.Managers
{
    public class ZoneManager : MonoBehaviour, IInitializable, IRegisterable
    {
        [Header("Data References")] 
        [SerializeField] private List<ZoneData> _zoneDatas;

        [Header("Controller References")] 
        [SerializeField] private WheelController _wheelController;
        [SerializeField] private ZoneBarController _zoneBarController;
        [SerializeField] private RewardAreaController _rewardAreaController;

        private ZoneStateController _zoneStateController;

        #region Initialization and Deinitialization

        public void Initialize()
        {
            _zoneStateController = new ZoneStateController(_zoneDatas);
            _wheelController.Initialize();
            _zoneBarController.Initialize();
        }

        public void Deinitialize()
        {
            _wheelController.Deinitialize();
            _zoneBarController.Deinitialize();
        }

        #endregion

        #region Registration and Unregistration

        public void Register()
        {
            _wheelController.Register();
            _zoneBarController.Register();
            _rewardAreaController.Register();

            ObserverManager.Register<OnWheelSpinCompletedEvent>(OnWheelSpinCompleted);
        }

        public void Unregister()
        {
            _wheelController.Unregister();
            _zoneBarController.Unregister();
            _rewardAreaController.Unregister();

            ObserverManager.Unregister<OnWheelSpinCompletedEvent>(OnWheelSpinCompleted);
        }

        #endregion

        public void BeginGameSession()
        {
            ObserverManager.Notify(new InputBlockerEvent(true));
            
            _zoneStateController.ResetZoneIndex();
            ZoneData zoneData = _zoneStateController.FindCurrentZone();

            _wheelController.BeginGameSession(zoneData);
            _zoneBarController.BeginGameSession();
            _rewardAreaController.BeginGameSession(); 
        }

        private void OnWheelSpinCompleted(OnWheelSpinCompletedEvent spinEvent)
        {
            AdvanceToNextZone(spinEvent.RewardData, spinEvent.RewardAmount);
        }
        
        private void AdvanceToNextZone(RewardData rewardData, int rewardAmount)
        {
            UpdateZoneIndex();
            ZoneData currentZoneData = GetCurrentZoneData();
            NotifyRewardAndStartTask(currentZoneData, rewardData, rewardAmount);
        }

        private void UpdateZoneIndex()
        {
            _zoneStateController.UpdateCurrentZoneIndex();
        }

        private ZoneData GetCurrentZoneData()
        {
            return _zoneStateController.FindCurrentZone();
        }

        private async void NotifyRewardAndStartTask(ZoneData zoneData, RewardData rewardData, int rewardAmount)
        {
            ObserverManager.Notify(new OnRewardDetermined(zoneData, _zoneStateController.CurrentZoneIndex, rewardData, rewardAmount));
            await Task.Delay(200);
            TaskService.Instance.StartTaskProcessing();
        }
    }
}