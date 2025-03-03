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

        public override async Task ExecuteAsync()
        {
            await base.ExecuteAsync(); 
        }

        public override void Complete()
        {
            base.Complete(); // Base sınıftaki tamamlama işlemini çağır
        }
    }
}