using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VertigoGames.Controllers;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Events;
using VertigoGames.Interfaces;

namespace VertigoGames.Managers
{
    public class ZoneManager : MonoBehaviour, IInitializable, IRegisterable
    {
        [SerializeField] private WheelController _wheelController;
        [SerializeField] private ZoneStateController _zoneStateController;
        [SerializeField] private List<ZoneData> _zoneDatas;
        
        public void Initialize()
        {
            _zoneStateController = new ZoneStateController(_zoneDatas);
            _wheelController.Initialize();
        }

        public void Deinitialize()
        {
            _wheelController.Deinitialize();
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
            UpgradeZone();
        }
        
        private void OnWheelSpinCompleted(OnWheelSpinCompletedEvent obj)
        {
            UpgradeZone();
        }

        private async void UpgradeZone()
        {
            _zoneStateController.UpdateCurrentZoneIndex();
            ZoneData zoneData = _zoneStateController.FindCurrentZone();
            
            ObserverManager.Notify(new OnUpdateZoneDataEvent(zoneData.ZoneType, _zoneStateController.CurrentZoneIndex));
            await Task.Delay(300);

            ObserverManager.Notify(new OnZoneStateReadyEvent(zoneData));
        }
    }
}
