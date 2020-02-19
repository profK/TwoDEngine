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

       


        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public IDrawspace GetDrawspace(IRect2D subRect)
        {
            return new GtkDrawspace((Rect2D)subRect);
        }

        public IImage LoadImage(string path)
        {
            return (IImage)new Image(path);
        }

        public event Action<IGraphicsProvider> OnGraphicsInit;


        public void PushClip(IRect2D cipRect)
        {
            throw new System.NotImplementedException();
        }

        public IRect2D PopClip()
        {
            throw new System.NotImplementedException();
        }

        ~GraphicsProvider()
        {
            
        }

       
    }
}