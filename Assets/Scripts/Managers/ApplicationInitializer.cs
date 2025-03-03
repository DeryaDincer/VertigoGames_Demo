using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoGames.Interfaces;

namespace VertigoGames.Managers
{
    public class ApplicationInitializer : MonoBehaviour, IInitializable, IRegisterable
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private WindowManager _windowManager;
        
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

        public void Initialize()
        {
            _gameManager.Initialize();
            _windowManager.Initialize();
        }
        
        public void Deinitialize()
        {
            _gameManager.Deinitialize();
            _windowManager.Initialize();
        } 
        
        
        public void Register()
        {
            _gameManager.Register();
            _windowManager.Register();
        }
        
        public void Unregister()
        {
            _gameManager.Unregister();
            _windowManager.Unregister();
        }
      
    }
}