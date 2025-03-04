using System;
using UnityEngine;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Pooling;
using VertigoGames.Services;

namespace VertigoGames.Managers
{
    public sealed class GameManager : MonoBehaviour, IRegisterable
    {
        [Header("Manager References")] 
        [SerializeField] private ZoneManager _zoneManager;
        [SerializeField] private UIAnimationManager _uiAnimationManager;

        private ObjectPoolManager _objectPoolManager;
        private TaskService _taskService;
        #region Initialization and Deinitialization

        public void Initialize(ObjectPoolManager objectPoolManager, TaskService taskService)
        {
            _objectPoolManager = objectPoolManager;
            _taskService = taskService;
            
            _zoneManager.Initialize(_objectPoolManager, _taskService);
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