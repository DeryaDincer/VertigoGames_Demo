using VertigoGames.GameTasks;

namespace VertigoGames.Services
{
    public interface ITaskService
    {
        void StartTaskProcessing();
        void AddTask(ITask task);
        void CompleteTask(TaskType taskType);
        public void ClearTasks();
    }
}