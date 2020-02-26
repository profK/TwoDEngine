using System;
using ServiceRegistry;
using TwoDEngineCore;

namespace TDEGui
{
    public class GUIService:IGUIService
    {
        private IGraphicsProvider _graphicsProvider;
        
        public bool IsSupported()
        {
            _graphicsProvider = ServiceRegistry.Registry.GetService<IGraphicsProvider>();
            if (_graphicsProvider != null)
            {
                _graphicsProvider.OnGraphicsInit += GraphicsInit;
                return true;
            } else
            {
                return false;
            }
        }

       

        public event Action<IGUIService> OnGUInit;
        

        public IWindow GetWindow(string windowName, IRect2D windowRect)
        {
            return new Window(_graphicsProvider.GetDrawspace(windowRect));
        }

        public void GraphicsInit(IGraphicsProvider graphicsProvider)
        {
            OnGUInit?.Invoke(this);
        }
    }
}