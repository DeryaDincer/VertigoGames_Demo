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
        [SerializeField] private GameManager gameManager;
        [SerializeField] private WindowManager windowManager;
        [SerializeField] private ObjectPoolManager objectPoolManager;
        [SerializeField] private CurrencyManager currencyManager;
        private TaskService _taskService;

        #region Unity Lifecycle Methods
        private void Awake() =>  Initialize();

        private void OnEnable() =>  Register();

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
            gameManager.Initialize(objectPoolManager, _taskService, currencyManager);
            windowManager.Initialize(_taskService, currencyManager);
            objectPoolManager.Initialize();
            currencyManager.Initialize();
        }

        /// <summary>
        /// Deinitializes the core components of the application.
        /// </summary>
        public void Deinitialize()
        {
            gameManager.Deinitialize();
            windowManager.Deinitialize();
            objectPoolManager.Deinitialize();
            currencyManager.Deinitialize();
        }
        #endregion

        #region IRegisterable Implementation
        /// <summary>
        /// Registers the core components of the application.
        /// </summary>
        public void Register()
        {
            gameManager.Register();
            windowManager.Register();
            currencyManager.Register();
        }

        /// <summary>
        /// Unregisters the core components of the application.
        /// </summary>
        public void Unregister()
        {
            gameManager.Unregister();
            windowManager.Unregister();
            currencyManager.Unregister();
        }
        #endregion
    }
}