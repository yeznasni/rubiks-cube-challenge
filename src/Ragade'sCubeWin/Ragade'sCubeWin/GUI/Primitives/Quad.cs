using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using RagadesCubeWin.GraphicsManagement;
using RagadesCubeWin.Rendering;

namespace RagadesCubeWin.GUI.Primitives
{
    class RCQuad : RCSpatial
    {
        private float _width;
        private float _height;

        private Texture2D _image;

        private VertexPositionNormalTexture[] _vertices;
        private VertexDeclaration _vertexDeclaration;

        public float Width
        {
            get { return _width; }
            set 
            { 
                _width = value;
                UpdateVertices();
            }
        }

        public float Height
        {
            get { return _height; }
            set 
            { 
                _height = value;
                UpdateVertices();
            }
        }

        public Texture2D Image
        {
            get { return _image; }
            set { _image = value; }
        }


        public RCQuad()
            : base()
        {
            BuildGemetricData();
        }

        private void BuildGemetricData()
        {

            _vertices = new VertexPositionNormalTexture[6];
            
             UpdateVertices();
        }

        private void UpdateVertices()
        {
            Vector3 bottomLeft = new Vector3(0.0f, -_height, 0.0f);
            Vector3 topLeft = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 topRight = new Vector3(_width, 0.0f, 0.0f);
            Vector3 bottomRight = new Vector3(_width, -_height, 0.0f);

            // Fill in texture coordinates to display full texture
            // on quad
            Vector2 textureUpperLeft = new Vector2( 0.0f, 0.0f );
            Vector2 textureUpperRight = new Vector2( 1.0f, 0.0f );
            Vector2 textureLowerLeft = new Vector2( 0.0f, 1.0f );
            Vector2 textureLowerRight = new Vector2( 1.0f, 1.0f );

            // Provide a normal for each vertex
            for (int i = 0; i < _vertices.Length; i++)
            {
                _vertices[i].Normal = -Vector3.UnitZ;
            }

            // Set the position and texture coordinate for each
            // vertex
            _vertices[0].Position = bottomLeft;
            _vertices[0].TextureCoordinate = textureLowerLeft;
            _vertices[1].Position = topLeft;
            _vertices[1].TextureCoordinate = textureUpperLeft;
            _vertices[2].Position = bottomRight;
            _vertices[2].TextureCoordinate = textureLowerRight;
            _vertices[3].Position = bottomRight;
            _vertices[3].TextureCoordinate = textureLowerRight;
            _vertices[4].Position = topLeft;
            _vertices[4].TextureCoordinate = textureUpperLeft;
            _vertices[5].Position = topRight;
            _vertices[5].TextureCoordinate = textureUpperRight;

            
        }

 

        public override void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.VertexDeclaration = _vertexDeclaration;

            RCRenderManager.SetWorld(_worldTrans);

            if (_image != null)
            {
                RCRenderManager.TextureMappingEnabled(true);
                RCRenderManager.SetTexture(_image);
            }

            RCRenderManager.SetEffectMaterial(
                Vector3.One,
                Vector3.One,
                Vector3.Zero,
                0.0f,
                Vector3.Zero,
                1.0f
                );

            RCRenderManager.Render(
                graphicsDevice,
                OnRender
                );

            RCRenderManager.TextureMappingEnabled(false);

        }

        public override void LoadGraphicsContent(
            GraphicsDevice graphics,
            ContentManager content
            )
        {
            _vertexDeclaration = new VertexDeclaration(
                graphics,
                VertexPositionNormalTexture.VertexElements
                );
        }       
        
        public void OnRender(GraphicsDevice graphicsDevice)
        {
            
            // Draw two primitives
            graphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(
                PrimitiveType.TriangleList,
                _vertices,
                0, 2);

        }

        protected override void UpdateWorldBound()
        {
            
        }

        

    }
}
