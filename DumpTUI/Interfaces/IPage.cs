using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumpTUI.Interfaces
{
    /// <summary>
    /// IPage interface provides a structure for all pages to follow
    /// </summary>
    public interface IPage
    {
        /// <summary>
        /// The list of components on this page, each will be told to render
        /// </summary>
        public List<IComponent> Components { get; set; }
    }
}