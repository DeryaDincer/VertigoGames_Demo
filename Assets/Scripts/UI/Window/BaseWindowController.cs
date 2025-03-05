using VertigoGames.Events;
using VertigoGames.Managers;
using VertigoGames.Services;

namespace VertigoGames.UI.Window
{
    public abstract class BaseWindowController : Window
    {
        protected CurrencyManager CurrencyManager;
        private TaskService _taskService;

        public override void Initialize(TaskService taskService, CurrencyManager currencyManager)
        {
            base.Initialize(taskService, currencyManager);
            _taskService = taskService;
            CurrencyManager = currencyManager;
        }

        protected void CloseWindowWithTask(WindowType windowType, TaskType taskType)
        {
            WindowStateChangeInfo windowStateChangeInfo = new WindowStateChangeInfo
            {
                WindowType = windowType,
                ActiveStatus = false,
                CustomInfo = null
            };

            ObserverManager.Notify(new WindowStateChangedEvent(windowStateChangeInfo));
            _taskService.CompleteTask(taskType);
        }
    }
}