using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class ItemChampignon : Item
    {
        #region Fields
        private bool disposed; 

        #endregion

        #region Constructor
        public ItemChampignon( int X, int Y) : base(GetAnimation(), X, Y)
        {
            
        }
        #endregion

        #region Propertise

        #endregion

        #region Static Methods
        static Animation GetAnimation()
        {
            Texture2D SpriteCoins = Game1.game.Content.Load<Texture2D>("Images\\Texture\\Champignon");
            return new Animation(SpriteCoins, 8,false, 200);
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
            base.Dispose(disposing);
        }
        public override void PickUp(Player Player)
        {
            if (this.Bounds.Intersects(Player.Hitbox))
            {
                base.PickUp(Player);               
                Player.Score+=10;                
                if (Player.powerStateType != PowerStateType.White)
                    Player.Transformation(PowerStateType.Big);
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
            this.Animation.Draw(spriteBatch, this.Position);
        }
        #endregion

    }
}
