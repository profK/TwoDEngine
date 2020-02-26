using System;
using Gtk;
using ServiceRegistry;
using SkiaGraphicsProvider.Assets;
using SkiaSharp;
using TwoDEngineCore;
using TwoDEngineCore.Assets;
using TwoDEngineCore.Geometry;
using Image = SkiaGraphicsProvider.Assets.Image;


namespace SkiaGraphicsProvider
{
    public class GraphicsProvider:IGraphicsProvider,IService
    {
        private bool _initialized = false;
        #region Service Methods
        // service Methods
       public bool IsSupported()
       {
           try
           {
               Application.Init();
               _initialized = true;
           }
           catch (Exception e)
           {
               ServiceRegistry.Registry.Log(e.Message);
               return false;
           }

           return true;
       }

      
        #endregion
        
        public GraphicsProvider()
        {
           
           
        }
        
        public IPoint2D ScreenSize { get; }
        
        public void Start()
        {
            if (!_initialized)
            {
                Application.Init();
            }
            OnGraphicsInit?.Invoke(this);
            Application.Run();
        }
        

        public IDrawspace GetDrawspace(IRect2D subRect)
        {
            return new GtkDrawspace(this,(Rect2D)subRect);
        }

        public IImage LoadImage(string path)
        {
            return (IImage)new Image(path);
        }

        public IPoint2D MakePoint2D(float x, float y)
        {
            return new Point2D(x,y);
        }

        public IRect2D MakeRect2D(float x, float y, float w, float h)
        {
            return new Rect2D(x,y,w,h);
        }

        public IFont MakeFont(string fontName, int ptSize)
        {
            return new Font(fontName, ptSize);
        }

        


        public event Action<IGraphicsProvider> OnGraphicsInit;

        #region Geometry Factory

        public IMatrix2D GetIdentityXForm()
        {
            return new Matrix2D(SKMatrix.MakeIdentity());
        }

        public IMatrix2D GetRotationXform(float degrees)
        {
            return new Matrix2D(SKMatrix.MakeRotation(DegreesToRads(degrees)));
        }


        public IMatrix2D GetTranslationXform(IPoint2D translation)
        {
            return new Matrix2D(SKMatrix.MakeTranslation(translation.X,translation.Y));
        }
        
        public IMatrix2D CopyTransform(IMatrix2D source)
        {
            return new Matrix2D(source);
        }
        
        public static float DegreesToRads(float degrees)
        {
            return (float)(Math.PI * degrees / 180);
        }
        
        

        #endregion
    }
}