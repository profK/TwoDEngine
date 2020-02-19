using System.Numerics;
namespace TwoDEngineCore.Geometry
{
    public class Matrix2D:IMatrix2D
    {
        private Matrix3x2 _matrix;

        public Matrix2D()
        {
            _matrix = Matrix3x2.Identity;
            
        }

        public void Translate(IPoint2D delta)
        {
            _matrix = Matrix3x2.CreateTranslation(delta.X, delta.Y) * _matrix;
        }

        public void Rotate(float degrees)
        {
            _matrix = Matrix3x2.CreateRotation(degrees) * _matrix;
        }

        public void PreMultiply(IMatrix2D lhs)
        {
            
            _matrix = ((Matrix2D)lhs)._matrix * _matrix;
        }
    }
}