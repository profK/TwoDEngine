using System;
using System.Runtime.Intrinsics.X86;
using SkiaSharp;

namespace TwoDEngineCore.Geometry
{
    public class Rect2D:IRect2D
    {
        internal SKRect _SkRect;
        
        public Rect2D(float x, float y, float w, float h)
        {    
            _SkRect = new SKRect(x,y,x+w,y+h);
        }
        public Rect2D(SKRect r)
        {
            _SkRect = r;
        }

        public IPoint2D Position
        {
            get { return new Point2D(_SkRect.Left,_SkRect.Top); }
        }

        public IPoint2D Size
        {
            get { return new Point2D(_SkRect.Width,_SkRect.Height); }
        }
        
    }
}