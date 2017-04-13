using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ScalingMatrixTesting
{
    class ScreenManager : ScreenManager
    {

        private int virtualWidth;
        private int virtualHeight;
        private GraphicsDeviceManager graphicsDeviceManager;
        private bool updateMatrix = true;
        private Matrix scaleMatrix = Matrix.Identity;


        public Viewport Viewport { get { return new Viewport(0, 0, virtualWidth, virtualHeight); } }

        public Matrix Scale
        {
            get
            {
                if (updateMatrix)
                {
                    CreateScaleMatrix();
                    updateMatrix = false;
                }
                return scaleMatrix;
            }
        }

        public Matrix InputScale { get { return Matrix.Invert(Scale); } }

        //public Vector2 InputTranslate { get { return new Vector2(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y); } }

        public ScreenManager(Game game, int virtualWidth, int virtualHeight) : base(game)
        {
            //setup virtaul enviraonment
            this.virtualWidth = virtualWidth;
            this.virtualHeight = virtualHeight;
            this.graphicsDeviceManager = (GraphicsDeviceManager)game.Services.GetService(typeof(GraphicsDeviceManager));
        }

        protected void CreateScaleMatrix()
        {
            scaleMatrix = Matrix.CreateScale((float)GraphicsDevice.Viewport.Width / virtualWidth, (float)GraphicsDevice.Viewport.Width / virtualWidth, 1f);
        }

        public override void Draw(GameTime gameTime)
        {
            BeginDraw();
            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;
                screen.Draw(gameTime);
            }
        }

        protected void FullViewport()
        {
            Viewport vp = new Viewport();
            vp.X = vp.Y = 0;
            vp.Width = DeviceManager.PreferredBackBufferWidth;
            vp.Height = DeviceManager.PreferredBackBufferHeight;
            GraphicsDevice.Viewport = vp;
        }

        protected float GetVirtualAspectRatio()
        {
            return (float)virtualWidth / (float)virtualHeight;
        }

        protected void ResetViewport()
        {
            float targetAspectRatio = GetVirtualAspectRatio();
            // figure out the largest area that fits in this resolution at the desired aspect ratio     
            int width = DeviceManager.PreferredBackBufferWidth;
            int height = (int)(width / targetAspectRatio + .5f);
            bool changed = false;
            if (height & gt; DeviceManager.PreferredBackBufferHeight) {
                height = DeviceManager.PreferredBackBufferHeight;
                // PillarBox 
                width = (int)(height * targetAspectRatio + .5f);
                changed = true;
            }
            // set up the new viewport centered in the backbuffer 
            Viewport viewport = new Viewport();
            viewport.X = (DeviceManager.PreferredBackBufferWidth / 2) - (width / 2);
            viewport.Y = (DeviceManager.PreferredBackBufferHeight / 2) - (height / 2);
            viewport.Width = width;
            viewport.Height = height;
            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;
            if (changed)
            {
                updateMatrix = true;
            }
            DeviceManager.GraphicsDevice.Viewport = viewport;
        }

        protected void BeginDraw()
        {
            // Start by reseting viewport 
            FullViewport();
            // Clear to Black 
            GraphicsDevice.Clear(Color.Black);
            // Calculate Proper Viewport according to Aspect Ratio 
            ResetViewport();
            // and clear that    
            // This way we are gonna have black bars if aspect ratio requires it and     
            // the clear color on the rest 
            GraphicsDevice.Clear(Color.Black);
        }

    }
}
