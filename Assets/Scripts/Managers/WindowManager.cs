using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.Services;
using VertigoGames.UI.Window;

namespace VertigoGames.Managers
{
    /// <summary>
    /// Manages the opening, closing, and navigation between different windows in the application.
    /// </summary>
    public class WindowManager : MonoBehaviour, IRegisterable
    {
        [SerializeField] private List<Window> _windows;
        
        #region Initialization and Deinitialization
        public void Initialize(TaskService taskService, CurrencyManager currencyManager)
        {
            _windows.ForEach(window => window.Initialize(taskService, currencyManager));
        } 

        public void Deinitialize() => _windows.ForEach(window => window.Deinitialize());
        #endregion

        #region Registration and Unregistration
        public void Register()
        {
            _windows.ForEach(window => window.Register());
            ObserverManager.Register<WindowStateChangedEvent>(OnWindowActivationRequested);
        }

        public void Unregister()
        {
            _windows.ForEach(window => window.Unregister());
            ObserverManager.Unregister<WindowStateChangedEvent>(OnWindowActivationRequested);
        }
        #endregion

        #region Window Navigation
        private void OnWindowActivationRequested(WindowStateChangedEvent evt)
        {
            if (evt == null) return;

            Window targetWindow = GetWindowByType(evt.WindowStateChangeInfo.WindowType);
            if (targetWindow == null)
            {
                Debug.LogWarning($"Window of type {evt.WindowStateChangeInfo.WindowType} not found.");
                return;
            }

            SetWindowActive(targetWindow, evt.WindowStateChangeInfo.ActiveStatus, evt.WindowStateChangeInfo.CustomInfo);
        }

        private Window GetWindowByType(WindowType windowType)
        {
            return _windows.FirstOrDefault(window => window.WindowType == windowType);
        }

        private void SetWindowActive(Window window, bool isActive, object customData = null)
        {
            if (window == null)
            {
                Debug.LogWarning("Attempted to activate a null window.");
                return;
            }

            window.UpdateWindowVisibility(isActive, customData);
        }
        #endregion
    }
}