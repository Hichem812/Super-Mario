using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario // check this class 
{
    public enum EntityState { Idel,Lous, Throw, Walk, Run,StopRun, Jumping, Falling, Crouch , JumpOnTheWall }
    public class Entity : GameObject ,IDisposable
    {
        #region Fields
        private bool disposed;
        protected delegate void UpdateEntity(GameTime gameTime);
        public EntityState State;
        protected SpriteEffects Effects;
        protected UpdateEntity updateEntity;
        protected float JumpSpeed;
        public Entity(Sprite Sprite, float X, float Y) :
            base(Sprite, X, Y)
        {
        //this.velocity = this._Position;
        //this.velocity = Vector2.Zero;
        //this.Position = this._Position;
        }

        #endregion

        #region Propertise

        internal Sprite CurentAnimation
        {
            get { return /*(sprite)*/this.sprite; }
            set
            {
                this.sprite = value;
            }
        }
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
        public override void Destroy()
        {
            this.JumpSpeed = -500;
            this.State = EntityState.Lous;
            this.updateEntity = UpdateLoss;
          
            //((Animation) this.CurentAnimation).Zero();

            //this.State = EntityState.Loss;
        }
        //protected Animation InitialazeAnimation(Animation[] animations, EntityState state)
        //{
        //    foreach (Animation Anim in animations)
        //    {
        //        if (Anim.State == state)
        //        {
        //            return Anim;
        //        }
        //    }
        //    return null;
        //}
        #endregion

        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            this.updateEntity(gameTime);
        }
        protected virtual void UpdateLoss(GameTime gameTime)
        {
            ((AnimationLoss)this.CurentAnimation).Update(gameTime);
            if (((AnimationLoss)this.CurentAnimation).TheEnd())
                base.Destroy();

            this.Position = new Vector2(this.Position.X,this.Position.Y + (this.JumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds));
            this.JumpSpeed += 1300 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        
        public override void Draw(SpriteBatch SpriteBatch, GameTime gameTime)
        {
            this.CurentAnimation.Draw(SpriteBatch, this.Position, this.Effects,0.13f);
        }
        #endregion
    }
}
