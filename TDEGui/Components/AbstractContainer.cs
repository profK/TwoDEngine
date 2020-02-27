using System.Collections.Generic;
using TwoDEngineCore;

namespace TDEGui
{
    public abstract class AbstractContainer:AbstractComponent, IContainer
    {
        protected  List<IComponent> _children = new List<IComponent>();

        public override IPoint2D MinSize { get; }
        public override IPoint2D MaxSize { get; }
        public override IPoint2D PreferredSize { get; }
        public override IPoint2D CurrentSize {get; set; }
        
        public AbstractContainer(IContainer parent) : base(parent)
        {
            
        }
        
        public AbstractContainer(IDrawspace drawspace) : base(drawspace)
        {
            
        }
        
        
        public void AddComponent(IComponent component)
        {
            _children.Add(component);
        }

        public void RemoveComponent(IComponent component)
        {
            _children.Add(component);
        }

        public override void PaintComponent(IDrawspace drawspace)
        {
            foreach (IComponent child in _children)
            {
                child.PaintComponent(drawspace);
            }
        }

        public virtual void Layout()
        {
            foreach (IComponent child in _children)
            {
                (child as IContainer)?.Layout();
            }
        }
        

        public override void Update(long deltaTime)
        {
            foreach (IComponent child in _children)
            {
                child.Update(deltaTime);
            }
        }
    }
}