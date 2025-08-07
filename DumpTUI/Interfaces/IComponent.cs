using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumpTUI.Interfaces
{
    /// <summary>
    /// IComponent interface provides a structure for all components to follow
    /// 
    /// The component is responsible for determining whether or not it needs to be rendered
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// Renders the component
        /// </summary>
        /// <param name="dimensions">The latest console buffer dimensions</param>
        /// <param name="force">Whether or not to forcefully render, ignores any checks</param>
        public void Render(Dimensions dimensions, bool force);

        /// <summary>
        /// Whether or not the component is visible or not
        /// </summary>
        public bool IsVisible { get; set; }
    }

    /// <summary>
    /// IExtendedComponent interface extends the IComponent interface
    /// This extension adds a ComponentDimensions structure, which allows you to define a Width, Height, with an X and Y point
    /// </summary>
    public interface IExtendedComponent : IComponent
    {
        public ComponentDimensions Dimensions { get; set; }
    }

    /// <summary>
    /// Dimension structure
    /// </summary>
    public struct ComponentDimensions
    {
        public int Width;
        public int Height;

        public int X;
        public int Y;
    }
}