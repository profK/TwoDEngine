using System;
using System.Collections.Generic;
using TwoDEngineCore.Geometry;
using Gtk;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.Gtk;
using TwoDEngineCore;
using TwoDEngineCore.Assets;
using SkiaGraphicsProvider.Assets;
using Image = SkiaGraphicsProvider.Assets.Image;


namespace SkiaGraphicsProvider
{
   
    /// <summary>
    /// This implements the actual drawing functions ontop of GTK based Skia
    /// of note is the use of Canvas.Save and Canvas.Restore()
    /// Although this saves many things, most of them such as transform matrix are
    /// externally settable.  The notable exception is the Clipping rectangle which is ONLY
    /// subtractable.  The only way to reset it is to use the canvas save and restore, so we use it
    /// for that purpose and only that purpose in this API
    /// </summary>
    public class GtkDrawspace:IDrawspace
    {
        private Window _window;
        private SKDrawingArea _skiaView;
        private List<IPaintable> _paintables = new List<IPaintable>();
        private SKCanvas _currentCanvas;
        private SKTextAlign[] TEXTALIGN = new SKTextAlign[3];
        private SKSize _scaledSize;
        
        private Stack<SKMatrix> _xformStack = new Stack<SKMatrix>();
        
        public GtkDrawspace(Rect2D subrect)
        {
            _window = new Window("Drawspace");
            _window.DeleteEvent += OnWindowDeleteEvent;
            _window.SetDefaultSize((int)subrect.Size.X,(int)subrect.Size.Y);
            _window.Move((int)subrect.Position.X,(int)subrect.Position.Y);
            _skiaView = new SKDrawingArea();
            _skiaView.PaintSurface += OnPaintSurface;
            _skiaView.Show();
            _window.Child = _skiaView;
            _lastPaintTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            TEXTALIGN[(int)IDrawspace.HORIZONTAL_ALIGNMENT.LEFT]= SKTextAlign.Left;
            TEXTALIGN[(int)IDrawspace.HORIZONTAL_ALIGNMENT.CENTER] = SKTextAlign.Center;
            TEXTALIGN[(int)IDrawspace.HORIZONTAL_ALIGNMENT.RIGHT]= SKTextAlign.Right;
            _skiaView.PaintSurface += OnPaintSurface;
            // init matrix stack
            _xformStack.Push(SKMatrix.MakeIdentity());
            _window.ShowAll();
        }

        private long _lastPaintTime;
        #nullable enable
        private void OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
#nullable disable
        {
            
            // the the canvas and properties
            _currentCanvas = e.Surface.Canvas;
            //save the root state
            

            // get the screen density for scaling
            var scale = 1f;
            _scaledSize = new SKSize(e.Info.Width / scale, e.Info.Height / scale);

            // handle the device screen density
            _currentCanvas.Scale(scale);
            
            long currentTime =  DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            OnPaint?.Invoke(this,currentTime-_lastPaintTime);
            _lastPaintTime = currentTime;

        }

        private void OnWindowDeleteEvent(object o, DeleteEventArgs args)
        {
           //TODO dispase stuff
        }


        public void PushTransform(IMatrix2D xform)
        {
            _xformStack.Push(((Matrix2D)xform)._skMatrix);
        }

        public IMatrix2D PopTransform()
        {
           return new Matrix2D(_xformStack.Pop());
        }

        public IMatrix2D PeekTransform()
        {
            return new Matrix2D(_xformStack.Peek());
        }

        public void PushClip(IRect2D rect)
        {
            _currentCanvas.Save();
            _currentCanvas.ClipRect(((Rect2D)rect)._SkRect);
        }

        public void PopClip()
        {
            _currentCanvas.Restore();
        }

        public void DrawText(string text, IPoint2D position, int size=24, uint color=0xFF000000,
            IDrawspace.HORIZONTAL_ALIGNMENT align=IDrawspace.HORIZONTAL_ALIGNMENT.CENTER)
        {
            SKColor skColor = new SKColor(color);
            // draw some text
            var paint = new SKPaint
            {
                Color = skColor,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = TEXTALIGN[(int)align],
                TextSize = size
            };
            var coord = new SKPoint(_scaledSize.Width / 2, (_scaledSize.Height + paint.TextSize) / 2);
            _currentCanvas.DrawText(text, coord, paint);
        }

        public void DrawImage(IImage image, IRect2D source,IRect2D destination,byte alpha = 0xFF)
        {
            var paint = new SKPaint
            {
                Color = new SKColor(0xFF,0xFF,0xFF,alpha),
                IsAntialias = true
            };
            _currentCanvas.SetMatrix(_xformStack.Peek());
            _currentCanvas.DrawBitmap(((Image)image).bitMap,
                ((Rect2D)source).ToSkia(),((Rect2D)destination).ToSkia());
        }

        public void DrawImage(IImage image, IPoint2D position,byte alpha=0xFF)
        {
            _currentCanvas.SetMatrix(_xformStack.Peek());
            DrawImage(image,new Rect2D(0,0,image.Size.X,image.Size.Y),
                new Rect2D(position.X,position.Y,image.Size.X,image.Size.Y),
                alpha);
        }

        public IPoint2D Position { get; }
        public IPoint2D Size { get; }
        
      
        public event Action<IDrawspace,long> OnPaint;
        public event Action<IDrawspace> OnClose;

        //helps functions

    }
}