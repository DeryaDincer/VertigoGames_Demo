using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoGames.Interfaces;
using VertigoGames.UI.Window;


namespace VertigoGames.UI.Window
{
    public abstract class Window : MonoBehaviour, IWindow, IInitializable, IRegisterable
    {
        public abstract WindowType WindowType { get; }

        public virtual void OnWindowActivated(object customData){ }
        public virtual void OnWindowDeactivated() { }
        public virtual void Initialize(){ }
        public virtual void Deinitialize(){ }
        public virtual void Register(){ }
        public virtual void Unregister(){ }
        
        public void UpdateWindowVisibility(bool isOpen, object customData)
        {
            gameObject.SetActive(isOpen);

            if (isOpen)
            {
                OnWindowActivated(customData);
               
            }
            else
            {
                OnWindowDeactivated();
            }
        }
    }

}