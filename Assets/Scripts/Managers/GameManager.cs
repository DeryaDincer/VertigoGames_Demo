using Sirenix.OdinInspector;
using UnityEngine;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Pooling;
using VertigoGames.Services;
using VertigoGames.Settings;

namespace VertigoGames.Managers
{
    public sealed class GameManager : MonoBehaviour, IRegisterable
    {
        [Title("Manager References")] 
        [SerializeField] private ZoneManager zoneManager;
        [SerializeField] private UIAnimationManager uiAnimationManager;

        [Title("Settings References")] 
        [SerializeField] private GamePrefabSettings gamePrefabSettings;
        
        [Title("UI References")] 
        [SerializeField] private RectTransform gameUICanvas;
        
        private ObjectPoolManager _objectPoolManager;
        private ITaskService _taskService;
        
        #region Initialization and Deinitialization
        public void Initialize(ObjectPoolManager objectPoolManager, ITaskService taskService,CurrencyManager currencyManager)
        {
            _objectPoolManager = objectPoolManager;
            _taskService = taskService;
            
            zoneManager.Initialize(gamePrefabSettings, _objectPoolManager, _taskService, currencyManager, gameUICanvas);
            uiAnimationManager.Initialize(_objectPoolManager);
        }

        public void Deinitialize() { }
        #endregion

        #region Registration and Unregistration
        public void Register()
        {
            zoneManager.Register();
            uiAnimationManager.Register();
            BeginGameSession();

            ObserverManager.Register<GameSessionResetEvent>(OnGameSessionReset);
        }

        public void Unregister()
        {
            zoneManager.Unregister();
            uiAnimationManager.Unregister();

            ObserverManager.Unregister<GameSessionResetEvent>(OnGameSessionReset);
        }
        #endregion

        #region Game Flow
        private void BeginGameSession() =>  zoneManager.BeginGameSession();
       
        private void OnGameSessionReset(GameSessionResetEvent obj) =>  BeginGameSession();
        #endregion
    }
}