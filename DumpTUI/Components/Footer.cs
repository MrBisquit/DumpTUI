using DumpTUI.Interfaces;

namespace DumpTUI.Components
{
    public class Footer : IComponent
    {
        public bool IsVisible { get; set; } = false;

        public Dictionary<ConsoleKey, string> Keys { get; set; } = [];

        public bool ShowVersion;    // Whether or not to show the version
        public string Version       { get; set; } = string.Empty;
        public bool DevVersion;     // Whether or not this is a developer version
        public bool NewVersion;     // Whether or not there is a newer version

        public void Render(Dimensions dimensions, bool force)
        {
            // Reset the console
            Console.CursorTop = dimensions.Height;
            Console.CursorLeft = 0;

            // Set the console colours
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            // Attempt to render
        }
    }
}