namespace VertigoGames.UI.Window
{
    public interface IWindow 
    {
        public WindowType WindowType { get; }
        public void OnWindowActivated(object customData);
        public void OnWindowDeactivated();
    }
}