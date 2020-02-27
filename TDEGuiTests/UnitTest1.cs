
using NUnit.Framework;
using ServiceRegistry;
using SkiaGraphicsProvider;
using SkiaGraphicsProvider.Geometry;
using TDEGui;
using TwoDEngineCore;
using TwoDEngineCore.Assets;

namespace TDEGuiTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            //initialize system with SkiGTKProvider
            Registry.RegisterService(
                new GraphicsProvider());
        }

        [Test]
        public void TextTest()
        {
            IGraphicsProvider gp = Registry.GetService<IGraphicsProvider>();
            gp.OnGraphicsInit += provider =>
            {
                IWindow window = new Window(
                    provider.MakeRect2D(100, 100, 800, 600));
                IImage happyFace = provider.LoadImage("Assets/thumbs-up.png");
                GUIImage img = new GUIImage(window,happyFace);
                img.LocalXform.Translate(provider.MakePoint2D(100,100));
                IFont fnt = gp.MakeFont("courier", 28);
                Label testLabel = new Label(window, "Test Label",fnt);
                testLabel.LocalXform.Translate(provider.MakePoint2D(100,100));
            };
            gp.Start();
        }
    }
}