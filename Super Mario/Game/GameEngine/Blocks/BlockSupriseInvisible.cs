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
        //Color color;
        #endregion

        #region Constructor
        internal BlockSupriseInvisible(Item item, List<Item> items, List<BoundingBox> BoundinBoxList, float X, float Y) 
            : base(item, items, BoundinBoxList, X, Y)
        {
            //this.color = Color.White*0f;
        }
        #endregion

        #region Propertise

        #endregion

        #region Methods
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
