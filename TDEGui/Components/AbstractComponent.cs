using TwoDEngineCore;

namespace TDEGui
{
    public abstract class AbstractComponent:IComponent
    {

       
        public AbstractComponent(IContainer parent)
        {
            Parent = parent;
            SetDefaults();
            parent?.AddComponent(this);
        }
        
        public void SetDefaults(){
            LocalXform = Drawspace.Provider.GetIdentityXForm();
            LocalClip = Drawspace.Provider.MakeRect2D(
                0, 0, Drawspace.Size.X, Drawspace.Size.Y);
        }
        
        public AbstractComponent(IDrawspace drawspace)
        {
            LocalXform = drawspace.Provider.GetIdentityXForm();
            LocalClip = drawspace.Provider.MakeRect2D(
                0, 0, drawspace.Size.X, drawspace.Size.Y);
        }
        
        public void Paint(IDrawspace drawspace)
        {
            IMatrix2D copyXform = drawspace.Provider.CopyTransform(drawspace.PeekTransform());
            copyXform.PreMultiply(LocalXform);
            drawspace.PushTransform(copyXform);
            drawspace.PushClip(LocalClip);
            PaintComponent(drawspace);
            drawspace.PopClip();
            drawspace.PopTransform();
        }

        public abstract void PaintComponent(IDrawspace drawspace);

        public abstract void Update(long deltaTime);

        public virtual IPoint2D MinSize { get; }
        public virtual IPoint2D MaxSize { get; }
        public virtual IPoint2D PreferredSize { get; }
  
        public virtual IPoint2D CurrentSize { get; set; }
        public virtual IMatrix2D LocalXform { get; set; }
        public virtual IDrawspace Drawspace {
            get { return Parent.Drawspace; }
        }
        public virtual IRect2D LocalClip { get; set; }
        
        public IComponent Parent { get; }
    }
}