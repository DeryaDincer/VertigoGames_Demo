using System;
using System.Threading.Tasks;

namespace VertigoGames.GameTasks
{
    public interface ITask
    {
        TaskType TaskType { get; }

        Func<Task> TaskAction { get; set; }

        Task ExecuteAsync();

        void Complete();
    }
}
