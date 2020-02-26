using System;
using ServiceRegistry;
using TwoDEngineCore;

namespace TDEGui
{
    public interface IGUIService:IService
    {
        event Action<IGUIService> OnGUInit;

        public IWindow GetWindow(string windowName, IRect2D rect2D );
        
    }
}