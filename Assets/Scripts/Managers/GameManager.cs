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
        [SerializeField] private UIAnimationManager _uiAnimationManager;
        public void Initialize()
        {
            zoneManager.Initialize();
            _uiAnimationManager.Initialize();
        }
        
        public void Deinitialize() 
        {
            zoneManager.Deinitialize();
            _uiAnimationManager.Deinitialize();
        }
        
        public void Register()
        {
            zoneManager.Register();
            _uiAnimationManager.Register();
            StartGame();
        }
        
        public void Unregister()
        {
            zoneManager.Unregister();
            _uiAnimationManager.Unregister();
        }

        public void StartGame()
        {
            zoneManager.StartGame();
        }
    }
}