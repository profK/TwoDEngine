namespace TDEGui
{
    public interface IContainer:IComponent
    {
        public void AddComponent(IComponent component);
        public void RemoveComponent(IComponent component);

        public abstract void Layout();
    }
}