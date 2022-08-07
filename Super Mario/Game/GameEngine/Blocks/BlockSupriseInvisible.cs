using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class BlockSupriseInvisible : BlockSurprise
    {
        #region Fields
        private bool disposed;
        //Color color;
        #endregion

        #region Constructor
        internal BlockSupriseInvisible(Item item, List<Item> items, List<BoundingBox> BoundinBoxList, float X, float Y) 
            : base(item, items, BoundinBoxList, X, Y)
        {
            //BoundingBox bx = this.box;
            //this.box = new BoundingBoxBlockInvisible(this.box.Bounds, this);
            //this.BoundinBoxList.Remove(bx);
            //this.BoundinBoxList.Add(this.box);
           
        }
        protected override BoundingBoxBlock AddBox(Rectangle rec, Block block)
        {
            return new BoundingBoxBlockInvisible(rec, this);
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
        internal override void IsTouchingBottom(PowerStateType type)
        {
            if (this.item != null)
            {
                BoundingBox bx = this.box;
                this.box = new BoundingBoxBlock(this.box.Bounds, this);
                this.BoundinBoxList.Remove(bx);
                this.BoundinBoxList.Add(this.box);
            }
            base.IsTouchingBottom(type);
            
        }

        //internal override void IsTouchingBottom(PowerStateType type)
        //{
        //    base.IsTouchingBottom(type);
        //    if (this.item != null) this.color = Color.White;

        //}
        #endregion

        #region Update & Draw       
        internal override void DrawBase(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        #endregion

    }
}
