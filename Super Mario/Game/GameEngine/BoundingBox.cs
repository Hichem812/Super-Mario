using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using MyLibrary;

namespace Super_Mario
{
    internal class BoundingBox
    {
        #region Fields
        public Rectangle Bounds;
        private Tools tools;
        #endregion

        #region Constructor
        public BoundingBox(Rectangle bount)
        {
            this.Bounds = bount;
            this.tools = new Tools();
        }
        #endregion

        #region Propertise

        #endregion

        #region Static Methods
        public static bool IsTouchingTop(Rectangle Hitbox1, Rectangle Hitbox2)
        {
            return Hitbox1.Bottom > Hitbox2.Top
                && Hitbox1.Top < Hitbox2.Top
                && Hitbox1.Right > Hitbox2.Left
                && Hitbox1.Left < Hitbox2.Right;
        }
        #endregion

        #region Methods

        #region IsAttached
        public bool Intersects(Rectangle Hitbox)
        {
            return this.Bounds.Intersects(Hitbox);
        }
        public Vector2 IsAttachedToTheRight(Vector2 Position, Rectangle Hitbox)
        {
            return new Vector2(this.Bounds.X - Hitbox.Width,Position.Y);
        }
        public Vector2 IsAttachedToTheLeft(Vector2 Position, Rectangle Hitbox)
        {
            return new Vector2(this.Bounds.Right, Position.Y);
        }
        public Vector2 IsAttachedToTheTop(Vector2 Position, Rectangle Hitbox)
        {
            return new Vector2(Position.X,this.Bounds.Bottom/*+1*/);
        }
        public Vector2 IsAttachedToTheBottom(Vector2 Position,Rectangle Hitbox)
        {
            return new Vector2(Position.X, this.Bounds.Y - Hitbox.Height/*-1*/);
        }
        #endregion

        #region IsTouching

        public bool IsTouchingRight(Rectangle Hitbox/*, Rectangle Bounds=this.Bounds*/)
        {
            return this.tools.IsTouchingRight(Hitbox,this.Bounds);
            //return Hitbox.Left < this.Bounds.Right
            //    && Hitbox.Right > this.Bounds.Right
            //    && Hitbox.Bottom-15 > this.Bounds.Top
            //    && Hitbox.Top+15 < this.Bounds.Bottom;
        }
        public bool IsTouchingLeft(Rectangle Hitbox/*, Rectangle Bounds*/)
        {
            return this.tools.IsTouchingLeft(Hitbox,this.Bounds);
            //return Hitbox.Right > this.Bounds.Left
            //    && Hitbox.Left < this.Bounds.Left
            //    && Hitbox.Bottom-15 > this.Bounds.Top
            //    && Hitbox.Top+15 < this.Bounds.Bottom;
        }
        public bool IsTouchingTop(Rectangle Hitbox/*, Rectangle Bounds*/)
        {
            return this.tools.IsTouchingTop(Hitbox,this.Bounds);
            //return Hitbox.Bottom > this.Bounds.Top
            //    && Hitbox.Top < this.Bounds.Top
            //    && Hitbox.Right > this.Bounds.Left
            //    && Hitbox.Left < this.Bounds.Right;
        }
        public virtual bool IsTouchingBottom(Rectangle Hitbox, PowerStateType type)
        {
            return this.tools.IsTouchingBottom(Hitbox, this.Bounds);
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
