using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class EnemyKoopaTroopa : Enemy
    {
        #region Fields
        private bool disposed;
        private const float DefoltSpeed = 2f;
        private short Timer;
        private readonly short TimerDefoltValue;
        private List<Enemy> EnemyList;
        #endregion

        #region Constructor
        public EnemyKoopaTroopa(Rectangle pathway, List<Enemy> EnemyList)
            : base(GetAnimation(), pathway, DefoltSpeed)
        {
            this.EnemyList = EnemyList;
            this.TimerDefoltValue = 5000;
            this.Timer = TimerDefoltValue;            
        }

        #endregion

        #region Propertise

        #endregion

        #region Static Methods
        static Animation GetAnimation()
        {
            Animation anim = new Animation(Game1.game.Content.Load<Texture2D>("Images\\Entitys\\Enemy\\KoopaTroopa Walk")
               ,10 ,false, 60);
            return anim;
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
                //foreach (var item in this.EnemyList)
                //    item.Dispose();                
            }

            disposed = true;
            base.Dispose(disposing);
        }
        protected override void AddAnimations()
        {
            AnimationLoss Loss = new AnimationLoss(Game1.game.Content.Load<Texture2D>("Images\\Entitys\\Enemy\\KoopaTroopa Bypase"), 6, 100);
            Animation Bypase = new Animation(Game1.game.Content.Load<Texture2D>("Images\\Entitys\\Enemy\\KoopaTroopa Bypase"), 6,false, 100);
            Animation StaindUp = new Animation(Game1.game.Content.Load<Texture2D>("Images\\Entitys\\Enemy\\KoopaTroopa Staind Up"),20,true,100);
            
            this.animationsShift.Add(EntityState.Walk,GetAnimation());
            this.animationsShift.Add(EntityState.Lous, Loss);
            this.animationsShift[EntityState.Run] = Bypase;
            this.animationsShift[EntityState.Crouch] = Bypase;
            this.animationsShift[EntityState.Idel] = StaindUp;
        }
        private void Stop(GameTime gameTime)
        {
            this.State = EntityState.Crouch;
            this.updateEntity = UpdateStopRuning;
            this.CurentAnimation = this.animationsShift[this.State];
            this.CurentAnimation.Update(gameTime);
            this.player.ToJump(/*0,true*/);
            this.player.Position = new Vector2(this.player.Position.X, this.Hitbox.Top - this.player.Height - 1);
        }
        private void CheckColision(GameTime gameTime)
        {            
            if (this.tools.IsTouchingLeft(this.player.Hitbox, this.Hitbox))
            {
                this.player.IsHurt(gameTime);
                this.player.Push(new Vector2(-3, 0));
            }
            else if (this.tools.IsTouchingRight(this.player.Hitbox, this.Hitbox))
            {
                this.player.IsHurt(gameTime);
                this.player.Push(new Vector2(3, 0));
            }
            else if (this.tools.IsTouchingBottom(this.player.Hitbox, this.Hitbox))
            {
                this.player.IsHurt(gameTime);
                this.player.Push(new Vector2(0, -3));
            }
            else if (this.tools.IsTouchingTop(this.player.Hitbox, this.Hitbox))
            {
                int height = this.Height;
                Stop(gameTime);
                int dif = height - this.Height;
                this.Position = new Vector2(this.Position.X, this.Position.Y + dif);
            }
        }
        #endregion

        #region Update & Draw
        protected override void UpdateWalk(GameTime gameTime)
        {
            base.UpdateWalk(gameTime);
            CheckColision(gameTime);
            //else this.player.NotHurt();

        }
        private void UpdateWakeUp(GameTime gameTime)
        {           
            this.CurentAnimation.Update(gameTime);
            if (((Animation)this.CurentAnimation).TheEnd())
            {
                int height = this.Height;
                this.updateEntity = UpdateWalk;
                this.State = EntityState.Walk;

                this.CurentAnimation = this.animationsShift[this.State];
                this.CurentAnimation.Update(gameTime);

                int dif = this.Height - height;
                this.Position = new Vector2(this.Position.X, this.Position.Y - dif);
            }
            CheckColision(gameTime);
        }
        private void UpdateStopRuning(GameTime gameTime)
        {
            this.Timer -= ((short)gameTime.ElapsedGameTime.Milliseconds);
            if (this.Timer <=0)
            {
                this.Timer = this.TimerDefoltValue;
                int height = this.Height;

                this.State = EntityState.Idel;
                this.updateEntity = UpdateWakeUp;
                this.CurentAnimation = this.animationsShift[this.State];
                this.CurentAnimation.Update(gameTime);

                int dif = this.Height - height;
                this.Position = new Vector2(this.Position.X, this.Position.Y - dif);
                
                if (this.speed ==DefoltSpeed * 4) this.speed = DefoltSpeed;
                else if (this.speed == -DefoltSpeed * 4) this.speed = -DefoltSpeed;

            }            
            if (this.tools.IsTouchingLeft(this.player.Hitbox, this.Hitbox))
            {
                this.speed = DefoltSpeed * 4;
                this.Effects = SpriteEffects.FlipHorizontally;
                this.updateEntity = UpdateRuning;
                this.Position = new Vector2(this.player.Hitbox.Right + 1, this.Position.Y);
            }
            else if (this.tools.IsTouchingRight(this.player.Hitbox, this.Hitbox))
            {
                this.speed = -DefoltSpeed * 4;
                this.Effects = SpriteEffects.None;
                this.updateEntity = UpdateRuning;
                this.Position = new Vector2(
                    this.player.Hitbox.Left - this.player.Hitbox.Width - 1, this.Position.Y);
            }
            else if (this.tools.IsTouchingTop(this.player.Hitbox, this.Hitbox))
            {
                this.player.ToJump();
                this.updateEntity = UpdateRuning;
                if (this.speed > 0)
                {
                    this.speed = DefoltSpeed * 4;
                    this.Effects = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    this.speed = -DefoltSpeed * 4;
                    this.Effects = SpriteEffects.None;
                }
            }
            //else this.player.NotHurt();
        }
        private void UpdateRuning(GameTime gameTime)
        {
            base.UpdateWalk(gameTime);
            if (this.tools.IsTouchingBottom(this.player.Hitbox, this.Hitbox))
            {
                this.player.IsHurt(gameTime);
                this.player.Push(new Vector2(0, -3));
            }
            else if (this.tools.IsTouchingLeft(this.player.Hitbox, this.Hitbox) /*&& this.speed > 0*/)
            {
                this.player.IsHurt(gameTime);
                this.player.Push(new Vector2(-3, 0));
            }
            else if (this.tools.IsTouchingRight(this.player.Hitbox, this.Hitbox)/*&& this.speed < 0*/)
            {
                this.player.IsHurt(gameTime);
                this.player.Push(new Vector2(3, 0));
            }
            else if (this.tools.IsTouchingTop(this.player.Hitbox, this.Hitbox))
            {
                Stop(gameTime);
            }
            foreach (var enemy in this.EnemyList)
            {
                if(this == enemy) continue;
                if (this.Hitbox.Intersects(enemy.Hitbox))
                {
                    enemy.Destroy();
                }
            }
        }
     
        #endregion
    }
}
