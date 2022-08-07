using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using MyLibrary;

namespace Super_Mario
{
    internal class BoundingBox : IDisposable
    {
        #region Fields
        private bool disposed;
        public Rectangle Bounds;
        private Tools tools;
        #endregion

        #region Constructor
        public BoundingBox(Rectangle bount)
        {
            Bounds = bount;
            tools = new Tools();
        }
        #endregion

        #region Propertise

        #endregion

        #region Static Methods
        //public static bool IsTouchingTop(Rectangle Hitbox1, Rectangle Hitbox2)
        //{
        //    return Hitbox1.Bottom > Hitbox2.Top
        //        && Hitbox1.Top < Hitbox2.Top
        //        && Hitbox1.Right > Hitbox2.Left
        //        && Hitbox1.Left < Hitbox2.Right;
        //}
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
                this.tools.Dispose();
            }

            disposed = true;

        }

        #region IsAttached
        public bool Intersects(Rectangle Hitbox)
        {
            return Bounds.Intersects(Hitbox);
        }
        public Vector2 IsAttachedToTheRight(Vector2 Position, Rectangle Hitbox)
        {
            return new Vector2(Bounds.X - Hitbox.Width, Position.Y);
        }
        public Vector2 IsAttachedToTheLeft(Vector2 Position, Rectangle Hitbox)
        {
            return new Vector2(Bounds.Right, Position.Y);
        }
        public Vector2 IsAttachedToTheTop(Vector2 Position, Rectangle Hitbox)
        {
            return new Vector2(Position.X, Bounds.Bottom/*+1*/);
        }
        public Vector2 IsAttachedToTheBottom(Vector2 Position, Rectangle Hitbox)
        {
            return new Vector2(Position.X, Bounds.Y - Hitbox.Height/*-1*/);
        }
        #endregion

        #region IsTouching

        public virtual bool IsTouchingRight(Rectangle Hitbox/*, Rectangle Bounds=this.Bounds*/)
        {
            return tools.IsTouchingRight(Hitbox, Bounds);
            //return Hitbox.Left < this.Bounds.Right
            //    && Hitbox.Right > this.Bounds.Right
            //    && Hitbox.Bottom-15 > this.Bounds.Top
            //    && Hitbox.Top+15 < this.Bounds.Bottom;
        }
        public virtual bool IsTouchingLeft(Rectangle Hitbox/*, Rectangle Bounds*/)
        {
            return tools.IsTouchingLeft(Hitbox, Bounds);
            //return Hitbox.Right > this.Bounds.Left
            //    && Hitbox.Left < this.Bounds.Left
            //    && Hitbox.Bottom-15 > this.Bounds.Top
            //    && Hitbox.Top+15 < this.Bounds.Bottom;
        }
        public virtual bool IsTouchingTop(Rectangle Hitbox/*, Rectangle Bounds*/)
        {
            return tools.IsTouchingTop(Hitbox, Bounds);
            //return Hitbox.Bottom > this.Bounds.Top
            //    && Hitbox.Top < this.Bounds.Top
            //    && Hitbox.Right > this.Bounds.Left
            //    && Hitbox.Left < this.Bounds.Right;
        }
        public virtual bool IsTouchingBottom(Rectangle Hitbox, PowerStateType type)
        {
            return tools.IsTouchingBottom(Hitbox, Bounds);
            //return Hitbox.Top < this.Bounds.Bottom
            //    && Hitbox.Bottom > this.Bounds.Bottom
            //    && Hitbox.Right > this.Bounds.Left
            //    && Hitbox.Left < this.Bounds.Right;
        }
        #endregion


        #endregion

        #region Update & Draw

        //public void Draw (GameTime gameTime,SpriteBatch spriteBath)
        //{
        //    //spriteBath.Draw(Game1.game.Content.Load<Texture2D>("Images\\Entitys\\Mario\\Mario_Walk_Gm"), this.Bounds, Color.Red);
        //}
        #endregion
    }
}
