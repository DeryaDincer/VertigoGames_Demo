using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using VertigoGames.Events;
using VertigoGames.Interfaces;
using VertigoGames.UI.Window;

namespace VertigoGames.Managers
{
    public class WindowManager : MonoBehaviour
    {
        [SerializeField] private Window currentWindow;

        public List<Window> Windows => windows;
        [SerializeField] private List<Window> windows;
        
        #region Init

        public void Initialize() =>  windows.ForEach(window => window.Initialize());

        public void Deinitialize() =>   windows.ForEach(window => window.Deinitialize());

        #endregion

        #region SubscribeMethods

        public void Register()
        {
            windows.ForEach(window => window.Register());
            ObserverManager.Register<WindowActivateDataEvent>(NavigateToWindow);
        }

        public void Unregister()
        {
            windows.ForEach(window => window.Unregister());
            ObserverManager.Unregister<WindowActivateDataEvent>(NavigateToWindow);
        }
        

        #endregion

        private void NavigateToWindow(WindowActivateDataEvent evt)
        {
            bool status = evt.WindowActivateData.ActiveStatus;

            ActivateWindow(GetWindowOfType(evt.WindowActivateData.WindowType), status, evt.WindowActivateData.CustomData);

            if (evt.WindowActivateData.ActiveStatus)
                currentWindow = GetWindowOfType(evt.WindowActivateData.WindowType);
        }
        
        public Window GetWindowOfType(WindowType windowType) 
        {
            return windows.FirstOrDefault(window => window.WindowType == windowType);
        }

        public void ActivateWindow(Window window, bool status, object customData)
        {
            window.UpdateWindowVisibility(status, customData);
        }
    }
}

public struct WindowActivateData
{
    public WindowType WindowType;
    public bool ActiveStatus;
    public object CustomData;
    public WindowActivateData(WindowType windowType, bool active, object customData)
    {
        WindowType = windowType;
        ActiveStatus = active;
        CustomData = customData;
    }
}
