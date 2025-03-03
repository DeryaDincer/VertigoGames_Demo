using System;
using System.Threading.Tasks;
using UnityEngine;

namespace VertigoGames.TaskService
{
    public class ZoneBarTask : BaseTask
    {
        public ZoneBarTask(Func<Task> openZoneBarTaskAsync) 
            : base(TaskType.ZoneBar, openZoneBarTaskAsync)
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