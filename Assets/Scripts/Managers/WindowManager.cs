using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.UI.Window;

namespace VertigoGames.Managers
{
    /// <summary>
    /// Manages the opening, closing, and navigation between different windows in the application.
    /// </summary>
    public class WindowManager : MonoBehaviour, IInitializable, IRegisterable
    {
        [SerializeField] private List<Window> _windows;

        #region Initialization and Deinitialization
     
        public void Initialize() => _windows.ForEach(window => window.Initialize());

        public void Deinitialize() => _windows.ForEach(window => window.Deinitialize());

        #endregion

        #region Registration and Unregistration

        public void Register()
        {
            _windows.ForEach(window => window.Register());
            ObserverManager.Register<WindowStateChangeEvent>(OnWindowActivationRequested);
        }

        public void Unregister()
        {
            _windows.ForEach(window => window.Unregister());
            ObserverManager.Unregister<WindowStateChangeEvent>(OnWindowActivationRequested);
        }

        #endregion

        #region Window Navigation

        private void OnWindowActivationRequested(WindowStateChangeEvent evt)
        {
            if (evt == null) return;

            Window targetWindow = GetWindowByType(evt.WindowStateChangeInfo.WindowType);
            if (targetWindow == null)
            {
                Debug.LogWarning($"Window of type {evt.WindowStateChangeInfo.WindowType} not found.");
                return;
            }

            SetWindowActive(targetWindow, evt.WindowStateChangeInfo.ActiveStatus, evt.WindowStateChangeInfo.CustomData);
        }

        public Window GetWindowByType(WindowType windowType)
        {
            return _windows.FirstOrDefault(window => window.WindowType == windowType);
        }

        public void SetWindowActive(Window window, bool isActive, object customData = null)
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