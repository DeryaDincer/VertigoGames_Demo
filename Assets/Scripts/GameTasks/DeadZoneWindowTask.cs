using System;
using System.Threading.Tasks;
using UnityEngine;

namespace VertigoGames.GameTasks
{
    public class DeadZoneWindowTask : BaseTask
    {
        public DeadZoneWindowTask(Func<Task> openRewardAreaTaskAsync) 
            : base(TaskType.ShowDeadZoneWindow, openRewardAreaTaskAsync)
        {
        }
    }
}