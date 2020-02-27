using SkiaGraphicsProvider.Geometry;
using SkiaSharp;
using TwoDEngineCore;
using TwoDEngineCore.Assets;

namespace SkiaGraphicsProvider.Assets
{
    public class Font:IFont
    {
        internal SKPaint skPaint;

        public Font(string fontName,int ptSz)
        {
            skPaint = new SKPaint()
            {
                TextSize = ptSz,
                Typeface = SKTypeface.FromFamilyName(fontName)
            };
        }

        public int Pts
        {
            get { return (int)skPaint.TextSize; }
        }

        public IPoint2D GetTextSize(string text)
        {
            SKRect bounds = new SKRect();
            skPaint.MeasureText(text, ref bounds);
            return new Point2D(bounds.Width,bounds.Height);
        }

        public string Name
        {
            get { return skPaint.Typeface.FamilyName; }
        }
    }
}