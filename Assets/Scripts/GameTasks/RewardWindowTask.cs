using System;
using System.Threading.Tasks;

namespace VertigoGames.GameTasks
{
    public class RewardWindowTask : BaseTask
    {
        public RewardWindowTask(Func<Task> openRewardWindowAsync) 
            : base(TaskType.ShowRewardWindow, openRewardWindowAsync) { }
    }
}