using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Interfaces;

namespace VertigoGames.Managers
{
    public class GameManager : MonoBehaviour,IInitializable, IRegisterable
    {
        [SerializeField] private ZoneManager zoneManager;

        public void Initialize()
        {
            zoneManager.Initialize();
            StartGame();
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
            zoneManager.StartGame();
        }
    }
}