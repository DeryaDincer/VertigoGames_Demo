using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VertigoGames.Events;
using VertigoGames.GameTasks;
using VertigoGames.Managers;
using VertigoGames.Utility;

namespace VertigoGames.Services
{
    /// <summary>
    /// Manages the execution of tasks in a queue, ensuring they are processed in order.
    /// </summary>
    public class TaskService
    {
        private Queue<ITask> _taskQueue = new();
        private TaskType _currentCompletedTaskType;
        private bool _isProcessing;

        public void StartTaskProcessing()
        {
            if (!_isProcessing)
                ProcessTaskQueueAsync();
        }

        public void AddTask(ITask task)
        {
            if (!_taskQueue.Any(x => x.TaskType == task.TaskType))
            {
                _taskQueue.Enqueue(task);
            }

            _taskQueue = new Queue<ITask>(_taskQueue.OrderBy(x => x.TaskType));
        }

        public void CompleteTask(TaskType taskType)
        {
            _currentCompletedTaskType = taskType;
        }

        private async void ProcessTaskQueueAsync()
        {
            ObserverManager.Notify(new InputBlockerEvent(true));

            while (_taskQueue.Count > 0)
            {
                _isProcessing = true;

                _currentCompletedTaskType = TaskType.None;

                ITask currentTask = _taskQueue.Dequeue();

                await currentTask.ExecuteAsync();
                await WaitUntilAsync(() => _currentCompletedTaskType == currentTask.TaskType);

                currentTask.Complete();
            }

            ClearTasks();
        }

        private void ClearTasks()
        {
            _taskQueue.Clear();
            _isProcessing = false;
            ObserverManager.Notify(new InputBlockerEvent(false));
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