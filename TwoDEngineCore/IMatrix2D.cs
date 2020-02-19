namespace TwoDEngineCore
{
    public interface IMatrix2D
    {
        public void Translate(IPoint2D delta);
        public void Rotate(float degrees);

        public void PreMultiply(IMatrix2D lhs);
    }
}