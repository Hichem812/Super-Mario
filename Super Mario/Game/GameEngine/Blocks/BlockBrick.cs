using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class BlockBrick : Block
    {
        private delegate void DrawBrick(SpriteBatch spriteBatch,GameTime gameTime);
        #region Fields
        private List<BoundingBox> BoundinBoxList;
        private DrawBrick DrawingBrick;
        private brickBroken BrickBroken;
        private bool Initialazebroken;
        #endregion

        #region Constructor
        public BlockBrick(List<BoundingBox> BoundinBoxList,float X, float Y) : base(GetAnimation(), X, Y)
        {
            this.BoundinBoxList = BoundinBoxList;
            this.BoundinBoxList.Add(this.box);
            this.DrawingBrick = DrawEco;
            this.BrickBroken = null;
            this.Initialazebroken = true;
        }
        #endregion

        #region Propertise

        #endregion

        #region Static Methods
        private static Animation GetAnimation()
        {
            Texture2D SpriteCoins = Game1.game.Content.Load<Texture2D>("Images\\Texture\\Brick anime");
            return new Animation(SpriteCoins, 16, 60, 2000);
        }
        #endregion

        #region Methods
        public override void Destroy()
        {
           
            base.Destroy();
        }
        public override void OnDestroy()
        {
            // make the Item animation 
        }
        private void initialazeBroken()
        {
            this.BrickBroken = new brickBroken(this.Position,this);
            this.DrawingBrick = DrawBroken;
            this.BoundinBoxList.Remove(this.box);
            this.Initialazebroken = false;
        }
        #endregion

        #region Update & Draw
        internal override void UpdatePlayerBig(GameTime gameTime)
        {
            if (this.Initialazebroken) initialazeBroken();
            else this.BrickBroken.Update(gameTime);            
        }
        internal void DrawEco(SpriteBatch spriteBatch,GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }
        internal void DrawBroken(SpriteBatch spriteBatch,GameTime gameTime)
        {
            this.BrickBroken.Draw(spriteBatch);
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            this.DrawingBrick(spriteBatch, gameTime);
        }
        //public override void Update(GameTime gameTime)
        //{
        //    //arreter ici rander le box fonction com les bounding list
        //}
        #endregion



    }
}
