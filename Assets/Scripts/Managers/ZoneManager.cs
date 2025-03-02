using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Events;
using VertigoGames.Interfaces;

namespace VertigoGames.Managers
{
    public class ZoneManager : MonoBehaviour, IInitializable, IRegisterable
    {
        [SerializeField] private WheelController _wheelController;
        [SerializeField] private List<ZoneData> _zoneDatas;
        private int _currentZoneIndex;
        
        public void Initialize()
        {
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
            _wheelController.StartGame(_zoneDatas.FirstOrDefault());

            UpgradeZone();
        }
        
        private void OnWheelSpinCompleted(OnWheelSpinCompletedEvent obj)
        {
            UpgradeZone();
        }

        private async void UpgradeZone()
        {
            _currentZoneIndex++;
            
            ObserverManager.Notify(new OnUpdateZoneDataEvent(ZoneType.Normal, _currentZoneIndex));
            await Task.Delay(300);
            
            
            //ObserverManager.Notify(new OnZoneStateReadyEvent());
        }
    }
}
