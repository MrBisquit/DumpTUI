namespace DumpTUI
{
    /// <summary>
    /// The main screen element of the DumpTUI tool
    /// This contains components such as the page, version, title, and more including events
    /// </summary>
    public class Screen : IDisposable
    {
        /// <summary>
        /// Gets the current console dimensions
        /// </summary>
        /// <returns>The current console dimensions</returns>
        public Dimensions GetDimensions() => new Dimensions() {
            Width = Console.BufferWidth,
            Height = Console.BufferHeight
        };
        /// <summary>
        /// Compares two dimension structures
        /// </summary>
        /// <param name="A">Dimension structure A</param>
        /// <param name="B">Dimension structure B</param>
        /// <returns>If the two dimension structures are the same</returns>
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

        public Interfaces.IPage? ActivePage { get; private set; }
        public void SetActivePage(Interfaces.IPage activePage)
        {
            ActivePage = activePage;
        }
        internal List<Interfaces.IComponent> DefaultComponents { get; private set; } = [];

        /// <summary>
        /// Screen class initialiser
        /// </summary>
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
        
        /// <summary>
        /// Disposes of the current Screen object
        /// </summary>
        public void Dispose()
        {
            ResizeTask.Dispose();
            DeregisterHandles();
        }
        /// <summary>
        /// Screen class deconstructor
        /// (Runs Dispose)
        /// </summary>
        ~Screen() => Dispose();

        /// <summary>
        /// Resize event handle
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="dimensions">The new console dimensions</param>
        public void ResizeHandle(object? sender, Dimensions dimensions)
        {
            Console.Clear(); // Clear the screen to ensure no issues arise
            Dimensions = dimensions;
            if(RenderEvent != null) RenderEvent(this, Dimensions);
        }

        /// <summary>
        /// Render event handle
        /// </summary>
        /// <param name="sender">Senser object</param>
        /// <param name="dimensions">The console dimensions</param>
        public void RenderHandle(object? sender, Dimensions dimensions)
        {
            // Render all of the default components
        }

        /// <summary>
        /// Registers handles
        /// </summary>
        internal void RegisterHandles()
        {
            ResizeEvent += ResizeHandle;
            RenderEvent += RenderHandle;
        }

        /// <summary>
        /// Deregisters handles
        /// </summary>
        internal void DeregisterHandles()
        {
            ResizeEvent -= ResizeHandle;
            RenderEvent -= RenderHandle;
        }

        /// <summary>
        /// Register default components
        /// </summary>
        internal void RegisterDefaultComponents()
        {
            DefaultComponents.Add(new Components.Header());
        }
    }

    /// <summary>
    /// Dimension structure
    /// </summary>
    public struct Dimensions
    {
        public int Width;
        public int Height;
    }
}