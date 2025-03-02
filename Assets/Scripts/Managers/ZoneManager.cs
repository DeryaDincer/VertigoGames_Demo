using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VertigoGames.Controllers;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Controllers.Zone;
using VertigoGames.Events;
using VertigoGames.Interfaces;

namespace VertigoGames.Managers
{
    public class ZoneManager : MonoBehaviour, IInitializable, IRegisterable
    {
        [SerializeField] private WheelController _wheelController;
        [SerializeField] private List<ZoneData> _zoneDatas;
       
        private ZoneStateController _zoneStateController;

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
            ObserverManager.Register<SpinWheelEvent>(OnSpinWheel);
        }

        public void Unregister()
        {
            _wheelController.Unregister();
            ObserverManager.Unregister<OnWheelSpinCompletedEvent>(OnWheelSpinCompleted);
            ObserverManager.Unregister<SpinWheelEvent>(OnSpinWheel);
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
            
            ObserverManager.Notify(new OnUpdateZoneDataEvent(zoneData, _zoneStateController.CurrentZoneIndex));

            await Task.Delay(300);
            
            ObserverManager.Notify(new InputBlockerEvent(false));
            ObserverManager.Notify(new OnZoneStateReadyEvent(zoneData));
        }

        private void OnSpinWheel(SpinWheelEvent obj)
        {
            return;
            ObserverManager.Notify(new InputBlockerEvent(true));
        }
    }
}
