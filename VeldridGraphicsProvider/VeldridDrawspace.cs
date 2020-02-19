using System.Text;
using TwoDEngineCore;
using TwoDEngineCore.Geometry;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.SPIRV;
using Veldrid.StartupUtilities;

namespace VeldridGraphicsProvider
{
    public class VeldridDrawspace:IDrawspace
    {
        
        //Veldrid
        public ResourceFactory Factory { get; private set; }

        public GraphicsDevice GraphicsDevice
        {
            get { return _graphicsDevice; }
        }

        public CommandList CommandList
        {
            get { return _commandList; }
        }

        public Pipeline Pipeline {
            get {
                return _pipeline;
            }
        }

        private GraphicsDevice _graphicsDevice;
        private CommandList _commandList;
        private Shader[] _shaders;
        private Pipeline _pipeline;
        private Sdl2Window _window;

        private const string VertexCode = @"
#version 450

layout(location = 0) in vec2 Position;
layout(location = 1) in vec4 Color;

layout(location = 0) out vec4 fsin_Color;

void main()
{
    gl_Position = vec4(Position, 0, 1);
    fsin_Color = Color;
}";

        private const string FragmentCode = @"
#version 450

layout(location = 0) in vec4 fsin_Color;
layout(location = 0) out vec4 fsout_Color;

void main()
{
    fsout_Color = fsin_Color;
}";

        public VeldridDrawspace(Sdl2Window window)
        {
            _window = window;
            _graphicsDevice = VeldridStartup.CreateGraphicsDevice(window);
            Factory = _graphicsDevice.ResourceFactory;
            ShaderDescription vertexShaderDesc = new ShaderDescription(
                ShaderStages.Vertex,
                Encoding.UTF8.GetBytes(VertexCode),
                "main");
            ShaderDescription fragmentShaderDesc = new ShaderDescription(
                ShaderStages.Fragment,
                Encoding.UTF8.GetBytes(FragmentCode),
                "main");
            _shaders = Factory.CreateFromSpirv(vertexShaderDesc, fragmentShaderDesc);
            // imag3e layout
               // Create pipeline
            GraphicsPipelineDescription pipelineDescription = new GraphicsPipelineDescription();
            pipelineDescription.BlendState = BlendStateDescription.SingleOverrideBlend;
            pipelineDescription.DepthStencilState = new DepthStencilStateDescription(
                depthTestEnabled: true,
                depthWriteEnabled: true,
                comparisonKind: ComparisonKind.LessEqual);
            pipelineDescription.RasterizerState = new RasterizerStateDescription(
                cullMode: FaceCullMode.Back,
                fillMode: PolygonFillMode.Solid,
                frontFace: FrontFace.Clockwise,
                depthClipEnabled: true,
                scissorTestEnabled: false);
            pipelineDescription.PrimitiveTopology = PrimitiveTopology.TriangleStrip;
            pipelineDescription.ResourceLayouts = System.Array.Empty<ResourceLayout>();
            VertexLayoutDescription vertexLayout = new VertexLayoutDescription(
                new VertexElementDescription("Position", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
                new VertexElementDescription("Color", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float4));
            pipelineDescription.ShaderSet = new ShaderSetDescription(
                vertexLayouts: new VertexLayoutDescription[] { vertexLayout },
                shaders: _shaders);
            pipelineDescription.Outputs = _graphicsDevice.SwapchainFramebuffer.OutputDescription;

            _pipeline = Factory.CreateGraphicsPipeline(pipelineDescription);

            _commandList = Factory.CreateCommandList();
        }

        public IPoint2D Position {
            get
            {
                return new Point2D(_window.X,_window.Y);
            }
        }

        public IPoint2D Size
        {
            get { return new Point2D((float) _window.Width, (float) _window.Height); }
        }

        public void BeginDraw()
        {
            // Begin() must be called before commands can be issued.
            _commandList.Begin();

            // We want to render directly to the output window.
            _commandList.SetFramebuffer(_graphicsDevice.SwapchainFramebuffer);
            _commandList.ClearColorTarget(0, RgbaFloat.Black);

        }

        

        public void EndDraw()
        {
             // End() must be called before commands can be submitted for execution.
             _commandList.End();
             _graphicsDevice.SubmitCommands(_commandList);
            
             // Once commands have been submitted, the rendered image can be presented to the application window.
             _graphicsDevice.SwapBuffers();
        }

        ~VeldridDrawspace()
        {
            
            _pipeline.Dispose();
            foreach (Shader shader in _shaders)
            {
                shader.Dispose();
            }
            _commandList.Dispose();
              
            
        }
    }

}