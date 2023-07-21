using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyLibrary;
using System;
using System.Collections.Generic;
using TiledSharp;
using Apos.Gui;
using FontStashSharp;


///--- remarques ----
///- ajouter les stages
///- ajouter le saut sur les mures


namespace Super_Mario
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        internal Camera camera;
        public Matrix transformMatrix;
        public static SpriteBatch spriteBatch;
        //public static ContentManager contentmanager;
        public static Game1 game;
        public static IMGUI UI;
        internal static Rectangle DestinationRec;
        internal RenderTarget2D renderTarget;
        public GameManager gameManager;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            game = this;           
        }

        //#region Propertise
        //internal Camera camera { get { return _camera; } set { this._camera = value; } }
        //#endregion
        protected override void Initialize()
        {
            Settings.SetOpionsScreen();

            this.renderTarget = new RenderTarget2D(GraphicsDevice,
               Settings.GameWidth, Settings.GameHeight);

            this.IsMouseVisible = Settings.IS_MOUSE_VISIBLE;

            FontSystem fontSystem = FontSystemFactory.Create(GraphicsDevice, 1024, 512);
            fontSystem.AddFont(TitleContainer.OpenStream($"{Content.RootDirectory}/Font/dogicapixel.ttf"));
            GuiHelper.Setup(this, fontSystem);
            UI = new IMGUI();

            this.gameManager = new GameManager(this);
            this.camera = new Camera();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);           

            //Content.RootDirectory = "Content";
            //content = Content;
            //graphicsDevice = GraphicsDevice;
            this.renderTarget = new RenderTarget2D(GraphicsDevice,
                   Settings.GameWidth, Settings.GameHeight);
        }
       
        //internal void InitializeContent()
        //{
        //    this.UnloadContent();
        //    this.LoadContent();
        //}


        protected override void UnloadContent()
        {            
            this.Content.Unload();
            
            //this.Content.Dispose() ;

            //this.Content.RootDirectory = null;

            // TODO: Unload any non ContentManager content here
        }



        protected override void Update(GameTime gameTime)
        {
            this.gameManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            DrawGameplay(gameTime);

            DrawRenderTarget(gameTime);

            UI.Draw(gameTime);

            base.Draw(gameTime);
        }

        private void DrawGameplay(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(GameManager.color);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, transformMatrix /*Matrix.CreateScale(Settings.PixelRatio)*/);

            this.gameManager.Draw(spriteBatch, gameTime);

            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
        }

        public void DrawRenderTarget(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            /// Ici, on doit savoir si on est en fullscreen ou pas.
            /// Pour pouvoir étendre le rendertarget aux bonnes dimensions
            //Rectangle rect;

            ////Si on est en fullscreen, on dessine le rendertarget à la dimension du téléviseur
            //if (Settings.IS_FULLSCREEN)
            //{
            //    rect = new Rectangle(0, 0, (int)Settings.windowsRes.X, (int)Settings.windowsRes.Y);
            //}
            ////Si on n'est pas en fullscreen, on dessine le rendertarget à la dimension à laquelle le jeu est développé
            //else
            //{
            //    rect = new Rectangle(0, 0, (int)Settings.GameWidth, (int)Settings.GameHeight);
            //}

            spriteBatch.Draw(renderTarget,DestinationRec /*rect*/, new Rectangle?(), Color.White, 0, new Vector2((float)(0), (float)(0)), SpriteEffects.None, 0.12f);

            spriteBatch.End();
        }
    }
}
