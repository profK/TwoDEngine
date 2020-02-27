using System.IO;
using SkiaGraphicsProvider.Geometry;
using SkiaSharp;
using TwoDEngineCore;
using TwoDEngineCore.Assets;

namespace SkiaGraphicsProvider.Assets
{
    public class Image:IImage
    {
        public SKBitmap bitMap;

        public Image(string path)
        {
            Stream fileStream = File.OpenRead (path);
            using (var stream = new SKManagedStream(fileStream))
                bitMap = SKBitmap.Decode(stream);
           
        }

        public IPoint2D Size
        {
            get
            {
                return new Point2D(bitMap.Width,bitMap.Height);
            }
        }
    }
}