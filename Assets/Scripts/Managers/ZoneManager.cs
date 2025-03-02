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
        [SerializeField] private List<ZoneData> zoneDatas;

        public void Initialize()
        {
        }

        public void Deinitialize()
        {
        }

        public void Register()
        {
        }

        public void Unregister()
        {
        }
        
        public void StartGame()
        {
            _wheelController.StartGame(zoneDatas.FirstOrDefault());
        }
    }
}
