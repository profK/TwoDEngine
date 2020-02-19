using System;
using System.Runtime.Intrinsics.X86;

namespace TwoDEngineCore.Geometry
{
    public class Rect2D:IRect2D
    {
        private Point2D _position;
        private Point2D _size;
        
        public Rect2D(float x, float y, float w, float h)
        {
            _position = new Point2D(x,y);
            _size = new Point2D(w,h);
        }

        public IPoint2D Position
        {
            get { return _position; }
        }

        public IPoint2D Size
        {
            get { return _size; }
        }
        
        
    }
}