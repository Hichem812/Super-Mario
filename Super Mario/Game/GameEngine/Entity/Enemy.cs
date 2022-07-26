using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using MyLibrary;

namespace Super_Mario
{
    internal abstract class Enemy : Entity
    {
        #region Fields
        protected Tools tools;
        protected Player player;
        Rectangle pathway;
        protected float speed;
        bool isFacingRight;
        protected Dictionary<EntityState, Sprite> animationsShift;
        #endregion

        #region Constuctor

        public Enemy(Animation sprit, Rectangle pathway, float speed = 2)
       :base(sprit,pathway.X,pathway.Y)
        {            
            Animation anim = sprit;
            if (anim.Width > pathway.Width || anim.Height > pathway.Height)
                throw new Exception("the hitbox is small then pathway");
            this.sprite = sprit;
            this.tools = new Tools();
            this.pathway = pathway;
            this.Position = new Vector2(pathway.X, pathway.Y);
            this.isFacingRight = true;
            this.speed = speed;
            this.Effects = SpriteEffects.FlipHorizontally;
            this.updateEntity = UpdateWalk;
            this.State = EntityState.Walk;
            this.animationsShift = new Dictionary<EntityState, Sprite>();
            this.AddAnimations();
        }
        #endregion

        #region Propertis
        //internal Animation animation { get { return (Animation)this.sprite; } }
        //public override Rectangle Hitbox { get { return new Rectangle((int)this.position.X, (int)this.position.Y, this.enemyAnim.Width, this.enemyAnim.Height); } }
        #endregion

        #region Methods
      
        protected abstract void AddAnimations();
        public override void Destroy()
        {
            base.Destroy();
            this.CurentAnimation = this.animationsShift[this.State];
        }
        protected virtual bool hasHit(Rectangle playerRect)
        {
            return this.Hitbox.Intersects(playerRect);
        }
        internal void SetPlayer(Player player)
        {
            this.player = player;
        }
        #endregion

        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            this.updateEntity(gameTime);
        }
        protected virtual void UpdateWalk(GameTime gameTime)
        {
            if (!pathway.Contains(this.Hitbox))
            {
                speed = -speed;
                isFacingRight = !isFacingRight;
                if (this.Effects == SpriteEffects.None) this.Effects = SpriteEffects.FlipHorizontally;
                else this.Effects = SpriteEffects.None;
            }
            
            this.Position = new Vector2(this.Position.X + speed, this.Position.Y);

            this.CurentAnimation = this.animationsShift[this.State];
            this.CurentAnimation.Update(gameTime);
        }
        //public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        //{
        //    //spriteBatch.Draw(this.enemyAnim.spriteSheet, this.pathway, Color.White * .5f);
        //    animation.Draw(spriteBatch, Position, this.Effects);
        //}
        #endregion
    }
}
