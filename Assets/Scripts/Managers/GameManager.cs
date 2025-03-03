using System;
using UnityEngine;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Events;
using VertigoGames.Interfaces;

namespace VertigoGames.Managers
{
    public sealed class GameManager : MonoBehaviour, IInitializable, IRegisterable
    {
        [Header("Manager References")] 
        [SerializeField] private ZoneManager _zoneManager;
        [SerializeField] private UIAnimationManager _uiAnimationManager;
        
        #region Initialization and Deinitialization

        public void Initialize()
        {
            _zoneManager.Initialize();
            _uiAnimationManager.Initialize();
        }

        public void Deinitialize()
        {
            _zoneManager.Deinitialize();
            _uiAnimationManager.Deinitialize();
        }

        #endregion

        #region Registration and Unregistration

        public void Register()
        {
            _zoneManager.Register();
            _uiAnimationManager.Register();
            BeginGameSession();

            ObserverManager.Register<GameSessionResetEvent>(OnGameSessionReset);
        }

        public void Unregister()
        {
            _zoneManager.Unregister();
            _uiAnimationManager.Unregister();

            ObserverManager.Unregister<GameSessionResetEvent>(OnGameSessionReset);
        }

        #endregion

        #region Game Flow

        private void BeginGameSession() =>  _zoneManager.BeginGameSession();
       
        private void OnGameSessionReset(GameSessionResetEvent obj) =>  BeginGameSession();

        #endregion
    }
}