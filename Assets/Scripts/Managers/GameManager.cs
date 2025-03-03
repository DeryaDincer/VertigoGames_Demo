using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Events;
using VertigoGames.Interfaces;

namespace VertigoGames.Managers
{
    public sealed class GameManager : MonoBehaviour,IInitializable, IRegisterable
    {
        [SerializeField] private readonly ZoneManager zoneManager;
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
            
            ObserverManager.Register<OnResetGameEvent>(OnResetGame);
        }
        
        public void Unregister()
        {
            zoneManager.Unregister();
            _uiAnimationManager.Unregister();
            
            ObserverManager.Unregister<OnResetGameEvent>(OnResetGame);
        }

        private void OnResetGame(OnResetGameEvent obj)
        {
            StartGame();
        }

        public void StartGame()
        {
            zoneManager.StartGame();
        }
    }
}