using System.Threading;
using TwoDEngineCore;
using TwoDEngineCore.Geometry;
using Xunit;

namespace VeldridGraphicsProvider.Tests
{
    public class TestFixture
    {
        [Fact]
        public void OpenWindow()
        {
            GraphicsProvider provider = new GraphicsProvider();
            IDrawspace drawspace = provider.GetDrawspace(new Rect2D(0,0,800,400));
            Assert.True(true, "Opened window");
            
        }
    }
}