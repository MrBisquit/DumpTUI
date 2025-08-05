namespace DumpTUI
{
    /// <summary>
    /// The main screen element of the DumpTUI tool
    /// This contains components such as the page, version, title, and more including events
    /// </summary>
    public class Screen : IDisposable
    {
        public Dimensions GetDimensions() => new Dimensions() {
            Width = Console.BufferWidth,
            Height = Console.BufferHeight
        };
        public bool CompareDimensions(Dimensions A, Dimensions B) =>
            A.Width == B.Width && A.Height == B.Height;

        // Events
        /// <summary>
        /// The event for rendering
        /// </summary>
        public EventHandler<Dimensions>? RenderEvent;
        /// <summary>
        /// The resize event
        /// </summary>
        public EventHandler<Dimensions>? ResizeEvent;

        public Task ResizeTask;
        public Dimensions Dimensions { get; private set; }

        public Screen()
        {
            RegisterHandles();
            ResizeTask = Task.Factory.StartNew(() =>
            {
                while(true)
                {
                    if (!CompareDimensions(GetDimensions(), Dimensions))
                    {
                        if(ResizeEvent != null) ResizeEvent(this, GetDimensions());
                    }

                    Thread.Sleep(500); // Sleep for 500 ms to ensure the thread doesn't get clogged up
                }
            });
            ResizeTask.Start();
        }
        
        public void Dispose()
        {
            ResizeTask.Dispose();
            DeregisterHandles();
        }
        ~Screen() => Dispose();

        public void ResizeHandle(object? sender, Dimensions dimensions)
        {
            Console.Clear(); // Clear the screen to ensure no issues arise
            Dimensions = dimensions;
            if(RenderEvent != null) RenderEvent(this, Dimensions);
        }

        public void RenderHandle(object? sender, Dimensions dimensions)
        {
            // Render all of the default components
        }

        internal void RegisterHandles()
        {
            ResizeEvent += ResizeHandle;
            RenderEvent += RenderHandle;
        }

        internal void DeregisterHandles()
        {
            ResizeEvent -= ResizeHandle;
            RenderEvent -= RenderHandle;
        }
    }

    public struct Dimensions
    {
        public int Width;
        public int Height;
    }
}