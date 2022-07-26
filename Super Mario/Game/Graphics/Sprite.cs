using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    public class Sprite // to check if I don't need it I'm going to delete it
    {

        #region Fields
        public Texture2D spriteSheet;

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

        #endregion

        #region Update & Draw
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatche, Vector2 Position,SpriteEffects Effects)
        {           
            spriteBatche.Draw(spriteSheet, Position, null, Color.White, 0f, new Vector2(), 1f, Effects, 1);
        }
        public virtual void Draw(SpriteBatch spriteBatche, Vector2 Position, Color color, SpriteEffects Effects)
        {
            spriteBatche.Draw(spriteSheet, Position, null, color, 0f, new Vector2(), 1f, Effects, 1);
        }
        #endregion

    }
}
