using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Super_Mario
{
    internal class BlockSurprise : Block
    {
        #region Fields

        delegate void SurpriseDrow(SpriteBatch spriteBatch, GameTime gameTime);
        delegate void SurpriseUpdate(GameTime gameTime);
        SurpriseDrow surpriseDrow;
        SurpriseUpdate surpriseUpdate;
        List<Item> items;
        protected Item item;
        private List<BoundingBox> BoundinBoxList;
        #endregion

        #region Constructor
        internal BlockSurprise(Item item, List<Item> items, List<BoundingBox> BoundinBoxList, float X, float Y) : base(GetAnimation(), X, Y)
        {
            this.item = item;
            this.items = items;
            this.BoundinBoxList = BoundinBoxList;
            this.BoundinBoxList.Add(this.box);
            this.surpriseDrow = DrawBase;
            this.surpriseUpdate = base.Update;
        }
        #endregion

        #region Propertise

        #endregion Methods
        internal override void IsTouchingBottom(PowerStateType type)
        {
            if (this.item != null) 
            {
                this.TouchingMouvment = -5;
                this.blockUpdate = UpdateJumpingBlock;
                this.item.Position = new Vector2(this.box.Bounds.X+5, this.box.Bounds.Y - this.box.Bounds.Height+10);
                this.items.Add(this.item);
                this.item = null;
                Texture2D Sprite = Game1.game.Content.Load<Texture2D>("Images\\Texture\\TextureClassic");
                this.sprite = new SpriteInternal(Sprite, 9, 6, 0, 1);
                this.surpriseDrow = this.DrawEmpty;
                this.surpriseUpdate = this.UpdateEmpty;
            }
        }
        #region Static Methods
        private static Animation GetAnimation()
        {
            Texture2D SpriteCoins = Game1.game.Content.Load<Texture2D>("Images\\Texture\\Surprise");
            return new Animation(SpriteCoins,7, 60, 2000);
        }
        #endregion
              
        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            this.surpriseUpdate(gameTime);
        }
        public void UpdateEmpty(GameTime gameTime)
        {
             
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.surpriseDrow(spriteBatch, gameTime);   
        }
        internal virtual void DrawBase(SpriteBatch spriteBatch,GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }
        internal void DrawEmpty(SpriteBatch spriteBatch,GameTime gameTime)
        {
            ((SpriteInternal)this.sprite).Draw(spriteBatch, this.Position,SpriteEffects.None);
        }
        #endregion

    }
}
