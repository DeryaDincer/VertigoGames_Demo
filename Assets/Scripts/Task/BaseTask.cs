using System;
using System.Threading.Tasks;
using UnityEngine;

namespace VertigoGames.TaskService
{
    public class BaseTask : ITask
    {
        public TaskType TaskType { get; }
        public Func<Task> TaskAction { get; set; }

        public BaseTask(TaskType taskType, Func<Task> taskAction)
        {
            TaskType = taskType;
            TaskAction = taskAction;
        }

        // ExecuteAsync metodu virtual olarak işaretlendi
        public virtual async Task ExecuteAsync()
        {
            if (TaskAction != null)
            {
                await TaskAction(); // TaskAction'ı await ile çağırın
            }
            else
            {
                throw new InvalidOperationException("TaskAction is null.");
            }
        }

        // Complete metodu virtual olarak işaretlendi
        public virtual void Complete()
        {
            // Görev tamamlandığında yapılacak işlemler
            Debug.Log($"Task of type {TaskType} completed.");
        }
    }
}