using System;
using System.Threading.Tasks;
using UnityEngine;

namespace VertigoGames.GameTasks
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

        public virtual async Task ExecuteAsync()
        {
            if (TaskAction != null)
            {
                await TaskAction(); 
            }
            else
            {
                throw new InvalidOperationException("TaskAction is null.");
            }
        }

        public virtual void Complete()
        {
            Debug.Log($"Task of type {TaskType} completed.");
        }
    }
}