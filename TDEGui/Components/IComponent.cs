using TwoDEngineCore;

namespace TDEGui
{
    public interface IComponent:IPaintable,IUpdatable
    {
        public IPoint2D MinSize { get; }
        public IPoint2D MaxSize { get; }
        public IPoint2D PreferredSize { get; }
        public IPoint2D CurrentSize { get; set; }
        public IMatrix2D LocalXform { get; set; }

        public IDrawspace Drawspace { get; }

        public void PaintComponent(IDrawspace drawspace);


    }
}