using System;
using System.Numerics;
using SkiaGraphicsProvider;
using SkiaSharp;

namespace TwoDEngineCore.Geometry
{
    public class Matrix2D:IMatrix2D
    {
        internal SKMatrix _skMatrix;

        public Matrix2D(SKMatrix matrix)
        {
            _skMatrix = matrix;
        }

        public void Translate(IPoint2D delta)
        {
            SKMatrix.PreConcat(ref _skMatrix,SKMatrix.MakeTranslation(delta.X,delta.Y));
        }

        public void Rotate(float degrees)
        {
            SKMatrix.PreConcat(ref _skMatrix,SKMatrix.MakeRotation(GraphicsProvider.DegreesToRads(degrees)));
        }

        public void PreMultiply(IMatrix2D lhs)
        {
            SKMatrix.PreConcat(ref _skMatrix,((Matrix2D)lhs)._skMatrix);
        }
    }
}