using System;
using System.Data;
using System.Runtime.Intrinsics.X86;
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
        
        public IMatrix2D GetIdentityXForm();

        public IMatrix2D GetRotationXform(float degrees);

        public IMatrix2D GetTranslationXform(IPoint2D translation);

        
        public IImage LoadImage(string path);

        public IPoint2D MakePoint2D(float x, float y);
        public IRect2D MakeRect2D(float x, float y, float w, float h);

        public IFont MakeFont(string fontName, int ptSize);

      

        IMatrix2D CopyTransform(IMatrix2D source);
    }
}