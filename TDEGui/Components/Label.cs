using TwoDEngineCore;
using TwoDEngineCore.Assets;

namespace TDEGui
{
    public class Label:AbstractComponent
    {
        private IFont _minFont;
        private IFont _maxFont;
        
        public Label(IContainer parent , string text, IFont font):base(parent)
        {
            Text = text;
            PreferredFont = CurrentFont = font;
            parent?.AddComponent(this);
        }

        public override void PaintComponent(IDrawspace drawspace)
        {
            drawspace.DrawText(Text,CurrentFont);
        }

        public override void Update(long deltaTime)
        {
            //nop
        }


        public override IPoint2D MinSize
        {
            get { return _minFont.GetTextSize(Text); }
        }

        public override IPoint2D MaxSize
        {
            get { return _maxFont.GetTextSize(Text); }
        }

        public override IPoint2D PreferredSize
        {
            get { return PreferredFont.GetTextSize(Text); }
        }

        public IPoint2D Size
        {
            get { return CurrentFont.GetTextSize(Text); }
        }

        

        public int MinPtSize { set; get; }
        public int MaxPtSize { set; get; }
        public int CurrentPtSize { get; private set; }
        public string Text { set; get; }
        public IFont PreferredFont { set; get; }
        public IFont CurrentFont { set; get; }
        
    }
}