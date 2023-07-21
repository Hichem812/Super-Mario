using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class EmptyBlock : BlockSurprise
    {
        #region Fields

        #endregion

        #region Constructor
        internal EmptyBlock(List<BoundingBox> BoundinBoxList, float X, float Y) :
            base(null, null, BoundinBoxList, X, Y)
        {
            Texture2D Sprite = Game1.game.Content.Load<Texture2D>("Images\\Texture\\TextureClassic");
            this.sprite = new SpriteInternal(Sprite, 9, 9, 0, 1);
            this.surpriseDrow = this.DrawEmpty;
            this.surpriseUpdate = this.UpdateEmpty;
        }
        #endregion

        #region Propertise

        #endregion

        #region Methods

        #endregion

        #region Update & Draw       

        #endregion
        
    }
}
