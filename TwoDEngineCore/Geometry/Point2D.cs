using System.Numerics;

namespace TwoDEngineCore.Geometry
{
    public class Point2D:IPoint2D
    {
        private Vector2 _vector2;
        public Point2D(float x, float y)
        {
           _vector2 = new Vector2(x,y);
        }

        public float X
        {
            get { return _vector2.X; }
        }

        public float Y
        {
            get { return _vector2.Y; }
        }
    }
}