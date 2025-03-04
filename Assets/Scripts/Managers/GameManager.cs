using System;
using UnityEngine;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Pooling;

namespace VertigoGames.Managers
{
    public sealed class GameManager : MonoBehaviour, IRegisterable
    {
        [Header("Manager References")] 
        [SerializeField] private ZoneManager _zoneManager;
        [SerializeField] private UIAnimationManager _uiAnimationManager;

        private ObjectPoolManager _objectPoolManager;
        #region Initialization and Deinitialization

        public void Initialize(ObjectPoolManager objectPoolManager)
        {
            _objectPoolManager = objectPoolManager;
            
            _zoneManager.Initialize(_objectPoolManager);
            _uiAnimationManager.Initialize(_objectPoolManager);
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