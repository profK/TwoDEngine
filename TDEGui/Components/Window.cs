using System;
using ServiceRegistry;
using TwoDEngineCore;

namespace TDEGui
{
    public class Window:AbstractContainer,IWindow
    {
       
        private long _lastPaintTime;
        private IDrawspace _drawspace;
        public Window(IDrawspace drawspace):base(drawspace) // no parent
        {
            _drawspace = drawspace;
            drawspace.OnPaint += DoCanvasPaint;
        }

        public Window(IRect2D subrect):
            this( Registry.GetService<IGraphicsProvider>().GetDrawspace(subrect)) // no parent
        {
            //nop
        }
        
        private void DoCanvasPaint(IDrawspace drawspace)
        {
            
                long currentTime =  DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                Update(currentTime-_lastPaintTime);
                _lastPaintTime = currentTime;
                Layout();
                Paint(drawspace);
        }

       
        public override IPoint2D MinSize
        {
            //todo probably temporary
            get {return _drawspace.Provider.MakePoint2D(1,1);}
        }

        public override IPoint2D MaxSize
        {
            get
            {
                return _drawspace.Provider.ScreenSize;
            }
        }

        public override IPoint2D PreferredSize
        {
            get
            {
                //windows prefer to stay the same size they already are
                return LocalPosition.Size;
            }
        }



        public IRect2D LocalPosition {
            get
            {
                IPoint2D pos = _drawspace.Position;
                IPoint2D sz = _drawspace.Size;
                return _drawspace.Provider.MakeRect2D(
                    pos.X,pos.Y,sz.X,sz.Y);
            }
            set
            {
                _drawspace.Position = value.Position;
                _drawspace.Size = value.Size;
            } 
        }
        
     
        
        
        public override IDrawspace Drawspace
        {
            get { return _drawspace; }
        }
    }
}