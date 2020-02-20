using SkiaSharp;
using TwoDEngineCore.Geometry;

namespace SkiaGraphicsProvider
{
    public static class SkiaTypeHelpers
    {
        public static SKRect ToSkia(this Rect2D r)
        {
            return new SKRect(r.Position.X,r.Position.Y,r.Size.X,r.Size.Y);
        }

        public static SKPoint ToSkia(this Point2D p)
        {
            return new SKPoint(p.X,p.Y);
        }

       
    }
}