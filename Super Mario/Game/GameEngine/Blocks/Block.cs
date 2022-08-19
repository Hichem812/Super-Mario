using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Super_Mario
{
    delegate void BlockUpdate(GameTime gameTime);
    internal abstract class Block : ObjectBase
    {
        #region Fields
        private bool disposed;
        protected BlockUpdate blockUpdate;
        protected BoundingBox box;
        protected private SByte TouchingMouvment;
        private short TimerMouvment;
        //private bool IsMouvment;
        #endregion

        #region Constructor
        internal Block(Animation animation, float X, float Y) : base(animation, new Vector2(X, Y))
        {            
            this.box = AddBox(new Rectangle((int)X, (int)Y, sprite.Width, sprite.Height), this);
            this.sprite = animation;
            this.TouchingMouvment = 0;
            this.TimerMouvment = 0;
            //this.IsMouvment = false;
            this.blockUpdate = UpdateEco;
        }
        protected virtual BoundingBoxBlock AddBox(Rectangle rec,Block block)
        {
            return new BoundingBoxBlock(rec, this);
        }
        #endregion

        #region Propertise
        public override bool IsDestroyed { get { return this.isDestroyed; } }
        internal Animation Animation
        {
            get { return (Animation)this.sprite; }
            set { this.sprite = value; }
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
        internal virtual void IsTouchingBottom(PowerStateType type)
        {
            if (type == PowerStateType.Small)
            {
                this.TouchingMouvment = -5;
                //this.IsMouvment = true;
                this.blockUpdate = UpdateJumpingBlock;
            }
            else
            {
                //this.Animation.color = Color.White * 0f; 
                this.blockUpdate = UpdatePlayerBig;
            }

        }
        public override void Destroy()
        {
            OnDestroy();
            this.isDestroyed = true;
        }
        public override void OnDestroy()
        {

        }
        #endregion

        #region Update & Draw
        internal void UpdateEco(GameTime gameTime)
        {
            this.Animation.Update(gameTime);
        }
        internal void UpdateJumpingBlock(GameTime gameTime)
        {
            this.Animation.Update(gameTime);
            //if (this.IsMouvment)
            //{
            this.TimerMouvment += (short)(gameTime.ElapsedGameTime.Milliseconds);
            if (this.TimerMouvment >= 30)
            {
                this.TimerMouvment = 0;
                this.Position = new Vector2(this.Position.X, this.Position.Y + this.TouchingMouvment);
                this.box.Bounds.X = (int)this.Position.X;
                this.box.Bounds.Y = (int)this.Position.Y;
                this.TouchingMouvment++;
                if (this.TouchingMouvment == 6)
                {
                    this.TouchingMouvment = -5;
                    this.blockUpdate = UpdateEco;
                    //this.IsMouvment = false;
                }
            }
            //}
        }
        internal virtual void UpdatePlayerBig(GameTime gameTime)
        {
            throw new Exception("");
        }
        public override void Update(GameTime gameTime)
        {
            this.blockUpdate(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.Animation.Draw(spriteBatch, this.Position,SpriteEffects.None,0.12f);
            //Texture2D SpriteCoins = Game1.game.Content.Load<Texture2D>("Images\\Texture\\Brick anime");
            //spriteBatch.Draw(SpriteCoins, this.box.Bounds, Color.White);
        }
        #endregion
    }
}
