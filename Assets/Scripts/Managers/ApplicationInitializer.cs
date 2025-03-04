using UnityEngine;
using VertigoGames.Interfaces;
using VertigoGames.Pooling;
using VertigoGames.Services;

namespace VertigoGames.Managers
{
    /// <summary>
    /// Manages the initialization and registration of core application components.
    /// </summary>
    public sealed class ApplicationInitializer : MonoBehaviour, IInitializable, IRegisterable
    {
        [Header("Manager References")] 
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private WindowManager _windowManager;
        [SerializeField] private ObjectPoolManager _objectPoolManager;
        [SerializeField] private CurrencyManager _currencyManager;
        private TaskService _taskService;

        #region Unity Lifecycle Methods

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            Register();
        }

        private void OnDisable()
        {
            Unregister();
            Deinitialize();
        }

        #endregion

        #region IInitializable Implementation

        /// <summary>
        /// Initializes the core components of the application.
        /// </summary>
        public void Initialize()
        {
            _taskService = new();
            _gameManager.Initialize(_objectPoolManager, _taskService, _currencyManager);
            _windowManager.Initialize(_taskService);
            _objectPoolManager.Initialize();
            _currencyManager.Initialize();
        }

        /// <summary>
        /// Deinitializes the core components of the application.
        /// </summary>
        public void Deinitialize()
        {
            _gameManager.Deinitialize();
            _windowManager.Deinitialize(); // Fixed: Previously was calling Initialize instead of Deinitialize
            _objectPoolManager.Deinitialize();
            _currencyManager.Deinitialize();
        }

        #endregion

        #region IRegisterable Implementation

        /// <summary>
        /// Registers the core components of the application.
        /// </summary>
        public void Register()
        {
            _gameManager.Register();
            _windowManager.Register();
            _currencyManager.Register();
        }

        /// <summary>
        /// Unregisters the core components of the application.
        /// </summary>
        public void Unregister()
        {
            _gameManager.Unregister();
            _windowManager.Unregister();
            _currencyManager.Unregister();
        }

        #endregion
    }
}