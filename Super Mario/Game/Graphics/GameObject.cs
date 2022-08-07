using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    public class GameObject : ObjectBase
    {

        #region Fields
        private bool disposed;
        public Rectangle Hitbox;
        #endregion

        #region Constructor

        public GameObject(Sprite Sprite, float X,float Y) :base(Sprite, new Vector2(X, Y))
        {
            Initialaze();
        }
        public GameObject(Sprite Sprite, Vector2 Position) : base(Sprite, Position)
        {
            Initialaze();
        }
        private void Initialaze()
        {
            this.Hitbox = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)Width, (int)Height);
        }
        #endregion

        #region Propertise
        public override Vector2 Position
        {
            get { return base.Position; }
            set
            {
              base.Position = value;
              this.Hitbox = new Rectangle((int)base.Position.X, (int)base.Position.Y, (int)Width, (int)Height);
            }
        }

        public override bool IsDestroyed { get { return this.isDestroyed; } } 
        //public Rectangle Hitbox { get { return new Rectangle((int)this._Position.X, (int)this._Position.Y, (int)Width, (int)Height); } }
        public virtual short Width { get { return this.sprite.Width; } }
        public virtual short Height { get { return this.sprite.Height; } }
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

        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }
        #endregion

    }
}
