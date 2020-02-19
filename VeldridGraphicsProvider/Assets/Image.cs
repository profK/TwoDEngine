using System.Numerics;
using TwoDEngineCore;
using TwoDEngineCore.Assets;
using TwoDEngineCore.Geometry;
using Veldrid;
using Veldrid.SPIRV;


namespace VeldridGraphicsProvider.Assets
{
    
    public struct VertexPositionTexture
    {
        public const uint SizeInBytes = 20;

        public float PosX;
        public float PosY;
        public float PosZ;

        public float TexU;
        public float TexV;

        public VertexPositionTexture(Vector3 pos, Vector2 uv)
        {
            PosX = pos.X;
            PosY = pos.Y;
            PosZ = pos.Z;
            TexU = uv.X;
            TexV = uv.Y;
        }
    }
    public class Image:IImage
    {
        
        public DeviceBuffer Vertices { get; private set; }
        public DeviceBuffer Indices { get; private set; }

        public Image(VeldridDrawspace dspace, Rect2D subRect)
        {
            BufferDescription ibDescription = new BufferDescription(
                4 * sizeof(ushort),
                BufferUsage.IndexBuffer);
            Indices = dspace.Factory.CreateBuffer(ibDescription);
            var vertices = new VertexPositionTexture[]
            {
                // Top
                new VertexPositionTexture(new Vector3(-0.5f, +0.5f, -0.5f), new Vector2(0, 0)),
                new VertexPositionTexture(new Vector3(+0.5f, +0.5f, -0.5f), new Vector2(1, 0)),
                new VertexPositionTexture(new Vector3(+0.5f, +0.5f, +0.5f), new Vector2(1, 1)),
                new VertexPositionTexture(new Vector3(-0.5f, +0.5f, +0.5f), new Vector2(0, 1)),
            };
            BufferDescription vbDescription = new BufferDescription(
                4 * VertexPositionTexture.SizeInBytes,
                BufferUsage.VertexBuffer);
            Vertices = dspace.Factory.CreateBuffer(vbDescription);
            dspace.GraphicsDevice.UpdateBuffer(Vertices, 0, vertices);

            ushort[] quadIndices = { 0, 1, 2, 3 };
            dspace.GraphicsDevice.UpdateBuffer(Indices, 0, quadIndices);
        }

        public void Draw(VeldridDrawspace dspace, IMatrix2D matrix)
        {
            dspace.CommandList.SetVertexBuffer(0, Vertices);
            dspace.CommandList.SetIndexBuffer(Indices, IndexFormat.UInt16);
            dspace.CommandList.SetPipeline(dspace.Pipeline);
            // Issue a Draw command for a single instance with 4 indices.
            dspace.CommandList.DrawIndexed(
                indexCount: 4,
                instanceCount: 1,
                indexStart: 0,
                vertexOffset: 0,
                instanceStart: 0);
        }

        ~Image()
        {
            Vertices.Dispose();
            Indices.Dispose();
        }
    }
}