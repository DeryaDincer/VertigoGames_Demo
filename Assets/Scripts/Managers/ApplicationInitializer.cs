using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoGames.Interfaces;
using VertigoGames.Pooling;

namespace VertigoGames.Managers
{
    public sealed class ApplicationInitializer : MonoBehaviour, IInitializable, IRegisterable
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private WindowManager _windowManager;
        [SerializeField] private ObjectPoolManager _objectPoolManager;
        private TaskService.TaskService _taskService;
        
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
            _taskService = new();
            _gameManager.Initialize();
            _windowManager.Initialize();
            _objectPoolManager.Initialize();
        }
        
        public void Deinitialize()
        {
            _gameManager.Deinitialize();
            _windowManager.Initialize();
            _objectPoolManager.Deinitialize();
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