using TwoDEngineCore;

namespace TDEGui
{
    public class Text:IComponent
    {
        public void Paint(IDrawspace drawspace)
        {
            throw new System.NotImplementedException();
        }

        public void Update(long deltaTime)
        {
            throw new System.NotImplementedException();
        }


        public IPoint2D MinSize { get; }
        public IPoint2D MaxSize { get; }
        public IPoint2D PreferredSize { get; }
        public IPoint2D Size { get; set; }
        public IMatrix2D LocalXform { get; set; }
    }
}