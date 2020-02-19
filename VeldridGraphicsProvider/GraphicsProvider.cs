using System.Collections.Generic;
using TwoDEngineCore;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace VeldridGraphicsProvider
{
    public class GraphicsProvider:IGraphicsProvider
    {
        private List<Sdl2Window> _windowList = new List<Sdl2Window>();
        
       // Veldrid
       private static GraphicsDevice _graphicsDevice;
       private static CommandList _commandList;
       private static DeviceBuffer _vertexBuffer;
       private static DeviceBuffer _indexBuffer;
       private static Shader[] _shaders;
       private static Pipeline _pipeline;
       
    
        
        public GraphicsProvider()
        {
            
        }
        public IPoint2D ScreenSize { get; }
        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public IDrawspace GetDrawspace(IRect2D subRect)
        {
            WindowCreateInfo windowCI = new WindowCreateInfo()
            {
                X = (int)subRect.Position.X,
                Y = (int) subRect.Position.Y,
                WindowWidth = (int)subRect.Size.X,
                WindowHeight = (int)subRect.Size.Y,
                WindowTitle = "Veldrid Tutorial"
            };
            Sdl2Window _window = VeldridStartup.CreateWindow(ref windowCI);
            _windowList.Add(_window);
            _graphicsDevice = VeldridStartup.CreateGraphicsDevice(_window);
            return new VeldridDrawspace(_window);
        }

        public void PushClip(IRect2D cipRect)
        {
            throw new System.NotImplementedException();
        }

        public IRect2D PopClip()
        {
            throw new System.NotImplementedException();
        }
    }
}