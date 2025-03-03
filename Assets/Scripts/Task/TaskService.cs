using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VertigoGames.Events;
using VertigoGames.Managers;

namespace VertigoGames.TaskService
{
    public class TaskService //singlekton ekle
    {
        public static TaskService Instance { get; private set; }

        private Queue<ITask> _taskQueue = new();
        private ITask _currentTask;
        private TaskType _currentCompletedTaskType;
        private TaskType _currentActionTaskType;
        private bool _isProcessing;

        public TaskService()
        {
            if (Instance != null)
            {
                Debug.LogWarning("You are trying to create another instance of TaskService.");
                return;
            }

            Instance = this;
        }

        public void StartTaskProcessing()
        {
            if (!_isProcessing)
                ProcessTaskQueueAsync();
        }

        private async void ProcessTaskQueueAsync()
        {
            ObserverManager.Notify(new InputBlockerEvent(true));
            
            while (_taskQueue.Count > 0)
            {
                _isProcessing = true;

                _currentCompletedTaskType = TaskType.None;
                _currentActionTaskType = TaskType.None;

                ITask currentTask = _taskQueue.Dequeue();
                _currentTask = currentTask;

                await currentTask.ExecuteAsync();

                _currentActionTaskType = currentTask.TaskType;

                await WaitUntilAsync(() => _currentCompletedTaskType == currentTask.TaskType);

                currentTask.Complete();
                _currentTask = null;
            }

            ClearTasks();
        }

        public void AddTask(ITask task)
        {
            if (!_taskQueue.Any(x => x.TaskType == task.TaskType))
            {
                _taskQueue.Enqueue(task);
            }

            _taskQueue = new Queue<ITask>(_taskQueue.OrderBy(x => x.TaskType));
        }

        private void ClearTasks()
        {
            Debug.LogError("ClearTasks input ac");
            _currentTask = null;
            _taskQueue.Clear();
            _isProcessing = false;
            ObserverManager.Notify(new InputBlockerEvent(false));
        }

        public void CompleteTask(TaskType taskType)
        {
            _currentCompletedTaskType = taskType;
        }

        public bool IsCurrentAction(TaskType taskType)
        {
            return _currentActionTaskType == taskType;
        }

        private async Task WaitUntilAsync(Func<bool> condition)
        {
            while (!condition())
            {
                await Task.Delay(10); // Küçük bir gecikme ekleyerek CPU kullanımını azaltır
            }
        }
    }
}