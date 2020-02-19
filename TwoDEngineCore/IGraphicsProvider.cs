using System;
using System.Data;
using TwoDEngineCore.Assets;

namespace TwoDEngineCore
{
    public interface IGraphicsProvider
    {
        #region Lifecycle
        event Action<IGraphicsProvider> OnGraphicsInit;
        
        #endregion
       
        public IPoint2D ScreenSize { get; }
        public IDrawspace GetDrawspace(IRect2D subRect);

        public void Start();

        
        public IImage LoadImage(string path);


       
    }
}