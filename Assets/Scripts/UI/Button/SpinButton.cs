using VertigoGames.Events;
using VertigoGames.Managers;

namespace VertigoGames.UI.Button
{
    public class SpinButton : BaseButton
    {
        protected override void OnButtonClicked() =>  ObserverManager.Notify(new WheelSpinStartedEvent());
    }
}