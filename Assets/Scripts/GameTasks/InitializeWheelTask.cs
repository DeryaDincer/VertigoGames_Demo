using System;
using System.Threading.Tasks;

namespace VertigoGames.GameTasks
{
    public class InitializeWheelTask : BaseTask
    {
        public InitializeWheelTask(Func<Task> openRewardAreaTaskAsync) 
            : base(TaskType.InitializeWheel, openRewardAreaTaskAsync)  { }
    }
}
