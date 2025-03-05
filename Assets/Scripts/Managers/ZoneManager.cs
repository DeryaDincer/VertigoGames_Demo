using System.Collections.Generic;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
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
        [Title("Data References")] 
        [SerializeField] private List<ZoneData> zoneDatas;

        public WheelController _wheelController;
        public ZoneBarController _zoneBarController;
        public RewardAreaController _rewardAreaController;
     
        private ZoneStateController _zoneStateController;
        private ITaskService _taskService;
        
        #region Initialization and Deinitialization
        public void Initialize(GamePrefabSettings gamePrefabSettings, 
            ObjectPoolManager objectPoolManager,
            ITaskService taskService,
            CurrencyManager currencyManager,
            RectTransform gameUICanvas)
        {
            _wheelController = Instantiate(gamePrefabSettings.WheelController, gameUICanvas);
            _zoneBarController = Instantiate(gamePrefabSettings.ZoneBarController, gameUICanvas);
            _rewardAreaController = Instantiate(gamePrefabSettings.RewardAreaController, gameUICanvas);
            
            _taskService = taskService;
            
            _zoneStateController = new ZoneStateController(zoneDatas);
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