using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class BlockSupriseFireInvisible : BlockSurpriseFire
    {

        #region Fields
        private bool disposed;
        #endregion

        #region Constructor
        internal BlockSupriseFireInvisible(Item itemChampignon, Item Segnditem, List<Item> items, List<BoundingBox> BoundinBoxList, float X, float Y)
            : base(itemChampignon, Segnditem, items, BoundinBoxList, X, Y)
        {
            //this.box = new BoundingBoxBlockInvisible(this.box.Bounds, this);
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
        #endregion

        #region Update & Draw       
        internal override void DrawBase(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }
        #endregion
    }
}
