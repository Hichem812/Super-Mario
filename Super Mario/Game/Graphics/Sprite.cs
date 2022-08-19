using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    public class Sprite :IDisposable // to check if I don't need it I'm going to delete it
    {

        #region Fields
        public Texture2D spriteSheet;
        private bool disposed;
        #endregion

        #region Constructor
        public Sprite(Texture2D Sprite)
        {
            this.spriteSheet = Sprite;
        }
        #endregion

        #region Propertise
        public virtual short Width { get { return (short)this.spriteSheet.Width; } }
        public virtual short Height { get { return (short)this.spriteSheet.Height; } }
        #endregion

        #region Methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                //this.spriteSheet.Dispose();
            }

            disposed = true;

        }
        #endregion

        #region Update & Draw
        public virtual void Update(GameTime gameTime)
        {

        }
     
        public virtual void Draw(SpriteBatch spriteBatche, Vector2 Position,SpriteEffects Effects, float layerDeft = 0f)
        {           
            spriteBatche.Draw(spriteSheet, Position, null, Color.White, 0f, new Vector2(), 1f, Effects, layerDeft);
        }
        public virtual void Draw(SpriteBatch spriteBatche, Vector2 Position, Color color, SpriteEffects Effects, float layerDeft = 0f)
        {
            spriteBatche.Draw(spriteSheet, Position, null, color, 0f, new Vector2(), 1f, Effects, layerDeft);
        }
        #endregion

    }
}
