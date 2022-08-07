using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary
{
    internal class Tools
    {


        #region Static Methods
        public static Vector2 GetSenter(Vector2 input)
        {
            return new Vector2(input.X/2, input.Y/2);
        }
        #endregion

        #region Methods

        #region Dispose
        private bool disposed;
        internal void Dispose()
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
              
            }

            disposed = true;

        }
        #endregion

        public bool IsTouchingRight(Rectangle Hitbox,Rectangle Hitbox2)
        {
            return Hitbox.Left < Hitbox2.Right
                && Hitbox.Right > Hitbox2.Right
                && Hitbox.Bottom - 15 > Hitbox2.Top
                && Hitbox.Top + 15 < Hitbox2.Bottom;
        }
        public bool IsTouchingLeft(Rectangle Hitbox, Rectangle Hitbox2)
        {
            return Hitbox.Right > Hitbox2.Left
                && Hitbox.Left < Hitbox2.Left
                && Hitbox.Bottom - 15 > Hitbox2.Top
                && Hitbox.Top + 15 < Hitbox2.Bottom;
        }
        public bool IsTouchingTop(Rectangle Hitbox, Rectangle Hitbox2)
        {
            return Hitbox.Bottom > Hitbox2.Top
                && Hitbox.Top < Hitbox2.Top
                && Hitbox.Right > Hitbox2.Left
                && Hitbox.Left < Hitbox2.Right;
        }
        public virtual bool IsTouchingBottom(Rectangle Hitbox, Rectangle Hitbox2)
        {
            return Hitbox.Top < Hitbox2.Bottom
                && Hitbox.Bottom > Hitbox2.Bottom
                && Hitbox.Right > Hitbox2.Left
                && Hitbox.Left < Hitbox2.Right;
        }
        #endregion
    }
}
