using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{ 
    internal abstract class Item : GameObject
    {
        #region Fields
        private bool disposed;
        #endregion

        #region Constructor
        protected Item(Animation animation, int X, int Y) : base(animation, X, Y)
        {
           
        }
        #endregion

        #region Propertise
        internal Rectangle Bounds { get { return this.Hitbox; } }
        public override short Width { get { return Animation.Width; } }
        public override short Height { get { return Animation.Height; } }
        internal Animation Animation { get { return (Animation)this.sprite; } }
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
        public virtual void PickUp(Player Player)
        {
            this.Destroy();
        }
        #endregion

        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            
        }
        public virtual void Update(GameTime gametime, Player Player)
        {          
            if (this.Bounds.Intersects(Player.Hitbox))
                PickUp(Player);
        }
        #endregion

    }
}
