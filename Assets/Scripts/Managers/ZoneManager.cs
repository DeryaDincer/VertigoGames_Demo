using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VertigoGames.Controllers;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Controllers.Zone;
using VertigoGames.Datas.Reward;
using VertigoGames.Events;
using VertigoGames.Interfaces;

namespace VertigoGames.Managers
{
    public class ZoneManager : MonoBehaviour, IInitializable, IRegisterable
    {
        [SerializeField] private WheelController _wheelController;
        [SerializeField] private List<ZoneData> _zoneDatas;
        [SerializeField] private ZoneBarController _zoneBarController;
       
        private ZoneStateController _zoneStateController;

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

        public void Register()
        {
            _wheelController.Register();
            
            ObserverManager.Register<OnWheelSpinCompletedEvent>(OnWheelSpinCompleted);
        }

        public void Unregister()
        {
            _wheelController.Unregister();
            ObserverManager.Unregister<OnWheelSpinCompletedEvent>(OnWheelSpinCompleted);
        }

        public void StartGame()
        {
            _zoneStateController.UpdateCurrentZoneIndex();
            ZoneData zoneData = _zoneStateController.FindCurrentZone();
            
           // ObserverManager.Notify(new OnUpdateZoneDataEvent(zoneData, _zoneStateController.CurrentZoneIndex));
            ObserverManager.Notify(new OnZoneStateReadyEvent(zoneData));
        }
        
        private void OnWheelSpinCompleted(OnWheelSpinCompletedEvent obj)
        {
            UpgradeZone(obj.RewardData, obj.RewardAmount);
        }

        private async void UpgradeZone(RewardData rewardData, int rewardAmount)
        {
            _zoneStateController.UpdateCurrentZoneIndex();
            ZoneData zoneData = _zoneStateController.FindCurrentZone();
            
            ObserverManager.Notify(new OnRewardDetermined(zoneData, _zoneStateController.CurrentZoneIndex,rewardData, rewardAmount));
            
            TaskService.TaskService.Instance.StartTaskProcessing();
            

            await Task.Delay(300);
            
            ObserverManager.Notify(new InputBlockerEvent(false));
            ObserverManager.Notify(new OnZoneStateReadyEvent(zoneData));
        }
    }
}
