using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class Animation_JumpOnTheWall : Sprite
    {
        #region Fields

        private bool disposed;
        byte frames, ColumnCurent, width, height;
        Rectangle rect;
        #endregion

        #region Constructor

        public Animation_JumpOnTheWall(Texture2D spritesheet, byte frames = 3) : base(spritesheet)
        {
            this.width = (byte)(spritesheet.Width / frames);
            this.height = (byte)spritesheet.Height;
            this.spriteSheet = spritesheet;
            this.frames = frames;
            this.ColumnCurent = 0;
            this.rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);    
        }

        #endregion

        #region Propertise

        #endregion

        #region Methods
        internal void NextFram()
        {
            this.ColumnCurent++;
            this.rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
        }
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
            base.Dispose(disposing);
        }
        #endregion

        #region Update & Draw       

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatche, Vector2 Position, Color color, SpriteEffects effect = SpriteEffects.None, float layerDeft = 0f)
        {
            //var rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
            spriteBatche.Draw(spriteSheet, Position, rect, color, 0f, new Vector2(), 1f, effect, layerDeft);
        }
        public override void Draw(SpriteBatch spriteBatche, Vector2 Position, SpriteEffects effect = SpriteEffects.None, float layerDeft = 0f)
        {
            //var rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
            spriteBatche.Draw(spriteSheet, Position, rect, Color.White, 0f, new Vector2(), 1f, effect, layerDeft);
        }

        #endregion

    }
}
