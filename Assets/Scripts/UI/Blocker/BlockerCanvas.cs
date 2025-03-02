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
            ObserverManager.Register<InputBlockerEvent>(InputBlocker);
        }

        private void OnDisable()
        {
            ObserverManager.Unregister<InputBlockerEvent>(InputBlocker);
        }

        private void InputBlocker(InputBlockerEvent obj)
        {
            _graphicRaycaster.enabled = obj.IsBlock;
        }
    } 
}

