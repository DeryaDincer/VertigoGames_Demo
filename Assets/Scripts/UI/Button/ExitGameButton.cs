using VertigoGames.Events;
using VertigoGames.Managers;

namespace VertigoGames.UI.Button
{
    public class ExitGameButton : BaseButton
    {
        protected override void OnButtonClicked() => ObserverManager.Notify(new GameSessionResetEvent());
    }
}
