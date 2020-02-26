namespace TwoDEngineCore.Assets
{
    public interface IFont
    {
        public string Name { get; }
        public int Pts { get; }
        public IPoint2D GetTextSize(string text);
    }
}