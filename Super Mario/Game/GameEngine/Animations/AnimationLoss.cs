using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using MyLibrary;

namespace Super_Mario
{
    internal class AnimationLoss : Sprite
    {
        #region Fields
        private bool disposed;
        private short Timer;
        private byte ColumnCurent, width, height, TimeToFinish;
        private float MilisocondsParFrams, Rotation;
        private Vector2 Origin;
        Rectangle rect;
        #endregion

        #region Constructor
        public AnimationLoss(Texture2D spritesheet, byte frames, float MilisocondsParFrams) 
            : base(spritesheet)
        {
            this.Rotation = 0;
            this.MilisocondsParFrams = MilisocondsParFrams;
            this.width = (byte)(spritesheet.Width / frames);
            this.height = (byte)spritesheet.Height;
            this.Origin = Tools.GetSenter(new Vector2(width, height));
            this.spriteSheet = spritesheet;
            
            this.Timer = 0;
            this.TimeToFinish = 30;
            this.ColumnCurent = 0;
            this.rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
        }
        #endregion

        #region Propertise
        public override short Width { get { return this.width; } }
        public override short Height { get { return this.height; } }
        #endregion

        #region Methods
        protected override void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
               
            }

            disposed = true;
            base.Dispose();
        }
        internal bool TheEnd()
        {
            return this.TimeToFinish <= 0;
        }
        #endregion

        #region Update & Draw       
        public override void Update(GameTime gameTime)
        {
            this.Timer += (short)(gameTime.ElapsedGameTime.Milliseconds);
            //var rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
            if (this.Timer >= this.MilisocondsParFrams)
            {
                this.Timer = 0;
                this.Rotation += 0.4f;
                this.TimeToFinish --;   
            }
            
        }
        public override void Draw(SpriteBatch spriteBatche, Vector2 Position, SpriteEffects effect = SpriteEffects.None, float layerDeft = 0f)
        {
            //var rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
            spriteBatche.Draw(spriteSheet, Position, rect, Color.White, this.Rotation, this.Origin, 1f, effect, layerDeft);
        }
        public override void Draw(SpriteBatch spriteBatche, Vector2 Position,Color color, SpriteEffects effect = SpriteEffects.None, float layerDeft = 0f)
        {        
            spriteBatche.Draw(spriteSheet, Position, rect, Color.White, this.Rotation, this.Origin, 1f, effect, layerDeft);
        }
        #endregion
    }
}
