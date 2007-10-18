using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace RagadesCubeWin.Input
{
    /// <summary>
    /// Watcher Interface
    ///     - just has the necessary functions guranteed that all
    ///     - watchers will have. 
    /// </summary>
    public interface IWatcher
    {
        bool DetectMyInput();
        bool WatchEvent(Input.Events.Event e);
        bool RemoveEvent(Input.Events.Event e);
        void RunEvents();
    }
}