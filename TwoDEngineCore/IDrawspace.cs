using System;
using TwoDEngineCore.Assets;

namespace TwoDEngineCore
{
    public interface IDrawspace
    {
        #region Lifecycle
        public event Action<IDrawspace,long> OnPaint;
        public event Action<IDrawspace> OnClose;
        #endregion

        #region Drawing Commands
        public enum HORIZONTAL_ALIGNMENT {LEFT,CENTER,RIGHT}
        public enum VERTICAL_ALIGNMENT {TOP, CENTER, BOTTOM}

        public void PushTransform(IMatrix2D xform);

        public IMatrix2D PopTransform();

        public IMatrix2D PeekTransform();

       
        public void DrawText(string text, IPoint2D position, int size = 24, uint color = 0xFF000000,
            IDrawspace.HORIZONTAL_ALIGNMENT align = IDrawspace.HORIZONTAL_ALIGNMENT.CENTER);

     
        public void DrawImage(IImage image, IRect2D source, IRect2D destination, byte alpha = 0xFF);

        public void DrawImage(IImage image, IPoint2D position, byte alpha = 0xFF);
        
        public void PushClip(IRect2D rect);

        public void PopClip();
        #endregion

    }
}