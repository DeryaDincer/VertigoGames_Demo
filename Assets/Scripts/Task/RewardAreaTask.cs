using System;
using System.Threading.Tasks;
using UnityEngine;

namespace VertigoGames.TaskService
{
    public class RewardAreaTask : BaseTask
    {
        public RewardAreaTask(Func<Task> openRewardAreaTaskAsync) 
            : base(TaskType.RewardArea, openRewardAreaTaskAsync)
        {
        }
    }
}