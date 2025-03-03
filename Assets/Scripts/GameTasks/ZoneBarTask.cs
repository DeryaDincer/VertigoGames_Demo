using System;
using System.Threading.Tasks;

namespace VertigoGames.GameTasks
{
    public class ZoneBarTask : BaseTask
    {
        public ZoneBarTask(Func<Task> openZoneBarTaskAsync) 
            : base(TaskType.ZoneBar, openZoneBarTaskAsync)
        {
        }
    }
}