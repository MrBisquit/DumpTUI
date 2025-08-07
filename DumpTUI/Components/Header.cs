using DumpTUI.Interfaces;

namespace DumpTUI.Components
{
    public class Header : IComponent
    {
        public bool IsVisible { get; set; } = false;

        public void Render(Dimensions dimensions, bool force)
        {
            
        }
    }
}
