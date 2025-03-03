using System;
using System.Threading.Tasks;
using UnityEngine;

namespace VertigoGames.TaskService
{
    public class RewardWindowTask : BaseTask
    {
        public RewardWindowTask(Func<Task> openRewardWindowAsync) 
            : base(TaskType.ShowRewardWindow, openRewardWindowAsync)
        {
        }

        public override async Task ExecuteAsync()
        {
            Debug.Log("Opening reward window...");
            await base.ExecuteAsync(); // Base sınıftaki asenkron işlemi çalıştır
            Debug.Log("Reward window opened.");
        }

        public override void Complete()
        {
            Debug.Log("Reward collected. Closing reward window...");
            base.Complete(); // Base sınıftaki tamamlama işlemini çağır
            Debug.Log("Reward window closed.");
        }
    }
}