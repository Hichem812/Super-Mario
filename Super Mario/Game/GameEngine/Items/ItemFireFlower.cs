using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class ItemFireFlower : Item
    {
        #region Fields
        private bool disposed;
        #endregion

        #region Constructor
        public ItemFireFlower(int X, int Y) : base(GetAnimation(), X, Y)
        {

        }
        #endregion

        #region Propertise

        #endregion

        #region Static Methods
        static Animation GetAnimation()
        {
            Texture2D SpriteCoins = Game1.game.Content.Load<Texture2D>("Images\\Texture\\Flawer");
            return new Animation(SpriteCoins, 8, false, 200);
        }
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
        public override void PickUp(Player Player)
        {
            if (this.Bounds.Intersects(Player.Hitbox))
            {
                base.PickUp(Player);
                Player.Score += 20;

                PowerStateType State;
                if (Player.powerStateType == PowerStateType.Small) State = PowerStateType.Big;
                else State = PowerStateType.White;

                Player.Transformation(State);
            }
        }
        #endregion

        #region Update & Draw
        public override void Update(GameTime gameTime, Player Player)
        {
            base.Update(gameTime, Player);
            this.Animation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.Animation.Draw(spriteBatch, this.Position,SpriteEffects.None,0.13f);
        }
        #endregion

    }
}
