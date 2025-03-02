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
        }
        
        public void Deinitialize()
        {
            _gameManager.Deinitialize();
        } 
        
        
        public void Register()
        {
            _gameManager.Register();
        }
        
        public void Unregister()
        {
            _gameManager.Unregister();
        }
      
    }
}