using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class BoundingBoxBlock : BoundingBox
    {
        #region Fields
        private bool disposed;
        Block block;
        #endregion

        #region Constructor
        public BoundingBoxBlock(Rectangle bount, Block block) : base(bount)
        {
            this.block = block;
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
                //this.block.Dispose();
            }

            disposed = true;

            base.Dispose(disposing);
        }

        public override bool IsTouchingBottom(Rectangle Hitbox, PowerStateType type)
        {
            bool Touching = base.IsTouchingBottom(Hitbox, type);
            if (Touching) block.IsTouchingBottom(type);
            return Touching;
        }

        #endregion

    }
}
