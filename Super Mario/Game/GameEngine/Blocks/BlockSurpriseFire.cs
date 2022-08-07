using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class BlockSurpriseFire : BlockSurprise
    {
        #region Fields
        private bool disposed;
        Item item2;
        #endregion

        #region Constructor
        internal BlockSurpriseFire(Item itemChampignon,Item Segnditem, List<Item> items, List<BoundingBox> BoundinBoxList, float X, float Y) 
            : base(Segnditem, items, BoundinBoxList, X, Y)
        {
            if (!(itemChampignon is ItemChampignon)) throw new Exception("the item while be a itemChampignon!");
            this.item2 = itemChampignon;
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
                item2.Dispose();
            }

            disposed = true;
            base.Dispose();
        }
        internal override void IsTouchingBottom(PowerStateType type)
        {
            if (type == PowerStateType.Small) this.item = this.item2;
            base.IsTouchingBottom(type);    
        }
        #endregion

        #region Update & Draw       

        #endregion

    }
}
