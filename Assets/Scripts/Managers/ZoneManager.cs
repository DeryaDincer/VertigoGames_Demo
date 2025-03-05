using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using VertigoGames.Controllers.Reward;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Controllers.Zone;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Pooling;
using VertigoGames.Services;
using VertigoGames.Settings;

namespace VertigoGames.Managers
{
    public class ZoneManager : MonoBehaviour, IRegisterable
    {
        [SerializeField] private GamePrefabSettings _gamePrefabSettings;
        
        [Header("Data References")] 
        [SerializeField] private List<ZoneData> _zoneDatas;

        [Header("Controller References")] 
        [SerializeField] private WheelController _wheelController;
        [SerializeField] private ZoneBarController _zoneBarController;
        [SerializeField] private RewardAreaController _rewardAreaController;
     
        private ZoneStateController _zoneStateController;
        private TaskService _taskService;
        
        #region Initialization and Deinitialization
        public void Initialize(ObjectPoolManager objectPoolManager, TaskService taskService, CurrencyManager currencyManager)
        {
            _taskService = taskService;
            
            _zoneStateController = new ZoneStateController(_zoneDatas);
            _wheelController.Initialize(objectPoolManager, taskService);
            _zoneBarController.Initialize(taskService);
            _rewardAreaController.Initialize(objectPoolManager, taskService, currencyManager);
        }
        #endregion

        #region Registration and Unregistration

        public void Register()
        {
            _wheelController.Register();
            _zoneBarController.Register();
            _rewardAreaController.Register();

            ObserverManager.Register<WheelSpinCompletedEvent>(OnWheelSpinCompleted);
        }

        public void Unregister()
        {
            _wheelController.Unregister();
            _zoneBarController.Unregister();
            _rewardAreaController.Unregister();

            ObserverManager.Unregister<WheelSpinCompletedEvent>(OnWheelSpinCompleted);
        }

        #endregion

        public void BeginGameSession()
        {
            _taskService.ClearTasks();
            ObserverManager.Notify(new InputBlockStateChangedEvent(true));
            
            _zoneStateController.ResetZoneIndex();
            ZoneData zoneData = _zoneStateController.FindCurrentZone();

            _wheelController.BeginGameSession(zoneData);
            _zoneBarController.BeginGameSession();
            _rewardAreaController.BeginGameSession(); 
        }

        private void OnWheelSpinCompleted(WheelSpinCompletedEvent spinEvent)
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
            if (rewardData.RewardInfo.RewardType == RewardType.Bomb)
            {
                ObserverManager.Notify(new DeadZoneRewardEvent());
            }
            else
            {
                ObserverManager.Notify(new RewardDeterminedEvent(zoneData, _zoneStateController.CurrentZoneIndex, rewardData, rewardAmount));
            }
            await Task.Delay(200);
            _taskService.StartTaskProcessing();
        }
    }
}