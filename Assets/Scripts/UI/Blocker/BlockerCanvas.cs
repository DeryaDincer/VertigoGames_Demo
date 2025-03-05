using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Events;
using VertigoGames.Managers;

namespace VertigoGames.UI.Blocker
{
    public class BlockerCanvas : MonoBehaviour
    {
        private GraphicRaycaster _graphicRaycaster => GetComponent<GraphicRaycaster>();
        private void OnEnable()
        {
            ObserverManager.Register<InputBlockStateChangedEvent>(InputBlocker);
        }

        private void OnDisable()
        {
            ObserverManager.Unregister<InputBlockStateChangedEvent>(InputBlocker);
        }

        private void InputBlocker(InputBlockStateChangedEvent obj)
        {
            _graphicRaycaster.enabled = obj.IsBlock;
        }
    } 
}

