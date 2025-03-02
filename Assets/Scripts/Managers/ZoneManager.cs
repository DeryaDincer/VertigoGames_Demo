using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Interfaces;

namespace VertigoGames.Managers
{
    public class ZoneManager : MonoBehaviour, IInitializable, IRegisterable
    {
        [SerializeField] private WheelController _wheelController;
        [SerializeField] private List<ZoneData> _zoneDatas;

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
        }

        public void Unregister()
        {
            _wheelController.Unregister();
        }
        
        public void StartGame()
        {
            _wheelController.StartGame(_zoneDatas.FirstOrDefault());
        }
    }
}
