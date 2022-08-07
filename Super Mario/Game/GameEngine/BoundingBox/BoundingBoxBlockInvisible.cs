using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class BoundingBoxBlockInvisible : BoundingBoxBlock
    {

        #region Fields
        private bool disposed;
        #endregion

        #region Constructor
        public BoundingBoxBlockInvisible(Rectangle Bounds, Block block) : base(Bounds, block)
        {

        }
        #endregion

        #region Propertise

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
        public override bool IsTouchingRight(Rectangle Hitbox)
        {
            return false;
        }
        public override bool IsTouchingLeft(Rectangle Hitbox)
        {
            return false;
        }
        public override bool IsTouchingTop(Rectangle Hitbox)
        {
            return false;
        }
        #endregion

        #region Update & Draw       

        #endregion
    }
}
