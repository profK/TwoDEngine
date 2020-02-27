using TwoDEngineCore;
using TwoDEngineCore.Assets;

namespace TDEGui
{
    public class GUIImage:AbstractComponent
    {
        private IImage _image;
        private IRect2D _sourceRect;
        private IRect2D _destRect;
        private byte _alpha = 255;


        public GUIImage(IContainer parent,IImage image,IRect2D srcRect = null) : base(parent)
        {
            _image = image;
            _sourceRect = srcRect != null
                ? srcRect
                : Drawspace.Provider.MakeRect2D(
                    0, 0, image.Size.X, image.Size.Y);
            _destRect = Drawspace.Provider.MakeRect2D(0, 0, image.Size.X, image.Size.Y);
        }

        public GUIImage(IDrawspace drawspace,IImage image, IRect2D srcRect = null) : base(drawspace)
        {
            _image = image;
            _sourceRect = srcRect != null
                ? srcRect
                : Drawspace.Provider.MakeRect2D(
                    0, 0, image.Size.X, image.Size.Y);
            _destRect = Drawspace.Provider.MakeRect2D(0, 0, image.Size.X, image.Size.Y);
        }

        public override void PaintComponent(IDrawspace drawspace)
        {
            drawspace.DrawImage(_image,_sourceRect,_destRect, _alpha);
        }

        public override void Update(long deltaTime)
        {
            //nop
        }
    }
}