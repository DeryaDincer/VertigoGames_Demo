using System;
using System.Threading.Tasks;
using UnityEngine;

namespace VertigoGames.GameTasks
{
    public class DangerRewardWindowTask : BaseTask
    {
        public DangerRewardWindowTask(Func<Task> openRewardAreaTaskAsync) 
            : base(TaskType.RewardArea, openRewardAreaTaskAsync)
        {
        }
    }
}