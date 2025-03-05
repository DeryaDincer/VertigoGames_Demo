using System;
using System.Threading.Tasks;

namespace VertigoGames.GameTasks
{
    public class RewardAreaTask : BaseTask
    {
        public RewardAreaTask(Func<Task> openRewardAreaTaskAsync) 
            : base(TaskType.RewardArea, openRewardAreaTaskAsync) { }
    }
}