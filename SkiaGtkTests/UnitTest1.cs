using System.Threading;
using NUnit.Framework;
using SkiaGraphicsProvider;
using TwoDEngineCore;
using TwoDEngineCore.Assets;
using TwoDEngineCore.Geometry;

namespace SkiaGtkTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void ShowDrawspace()
        {
            IDrawspace ds;
            GraphicsProvider gp = new GraphicsProvider();
            IImage tup = gp.LoadImage("Assets/thumbs-up.png");
            gp.OnGraphicsInit += provider =>
            {
                ds = provider.GetDrawspace(new Rect2D(100, 100, 800, 600));
                ds.OnPaint += (space, time) =>
                {
                    ds.DrawImage(tup,new Point2D(0,0));
                    space.PushClip(new Rect2D(100,100,600,400));
                    space.PushTransform(gp.GetRotationXform(30));
                    ds.DrawImage(tup,new Point2D(0,0));
                    ds.DrawImage(tup,new Rect2D(0,0,tup.Size.X/2,tup.Size.Y),
                        new Rect2D(100,100,200,200));
                    ds.DrawText("text test", new Point2D(100,100));
                    space.PopTransform();
                    space.PopClip();
                };
                
            };
            gp.Start();
            Assert.Pass();
        }
    }
}