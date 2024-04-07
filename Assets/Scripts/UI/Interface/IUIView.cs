

namespace Assets.Scripts.UI.Interface
{
    public interface IUIView
    {
        void SetController(IUIController controller);
        void ToggleUIView(bool value);
    }
}
