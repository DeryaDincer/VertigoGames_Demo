using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoGames.Interfaces;


namespace VertigoGames.UI.Window
{
    public interface IWindow 
    {
        public WindowType WindowType { get; }
        public void OnWindowActivated(object customData);
        public void OnWindowDeactivated();
    }

}