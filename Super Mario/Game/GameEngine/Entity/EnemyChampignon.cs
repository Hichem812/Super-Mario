using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class EnemyChampignon : Enemy
    {
        #region Fields
        private short TimeToDies;
        #endregion

        #region Constructor
        public EnemyChampignon(Rectangle pathway) : base(GetAnimation(), pathway, 1.8f)
        {
            this.TimeToDies = 2000;
        }

        #endregion

        #region Propertise

        #endregion

        #region Static Methods
        static Animation GetAnimation()
        {
            Animation anim = new Animation(Game1.game.Content.Load<Texture2D>("Images\\Entitys\\Enemy\\Enemy Champignon Walk")
               , 16, false, 40);
            return anim;
        }
        #endregion

        #region Methods
        
        protected override void AddAnimations()
        {
            AnimationLoss Loss = new AnimationLoss(Game1.game.Content.Load<Texture2D>("Images\\Entitys\\Enemy\\Enemy Champignon Walk"), 16, 100);
            Sprite Dies = new Sprite(Game1.game.Content.Load<Texture2D>("Images\\Entitys\\Enemy\\enemy champignon lous"));

            this.animationsShift.Add(EntityState.Walk, GetAnimation());
            this.animationsShift.Add(EntityState.Lous, Loss);
            this.animationsShift[EntityState.Crouch] = Dies;
        }
        #endregion

        #region Update & Draw       
        protected override void UpdateWalk(GameTime gameTime)
        {
            base.UpdateWalk(gameTime);

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
                this.State = EntityState.Crouch;
                this.updateEntity = UpdateDies;
                this.CurentAnimation = this.animationsShift[this.State];
                this.CurentAnimation.Update(gameTime);
                this.player.ToJump();
                this.player.Position = new Vector2(this.player.Position.X, this.Hitbox.Top - this.player.Height - 1);
                int dif = height - this.Height;
                this.Position = new Vector2(this.Position.X, this.Position.Y + dif);
            }
        }
        private void UpdateDies(GameTime gameTime)
        {
            this.TimeToDies -= (short)gameTime.ElapsedGameTime.Milliseconds;
            if (this.TimeToDies <= 0 || this.player.Hitbox.Intersects(this.Hitbox))
            {
                this.isDestroyed = true;
            }
        }
        #endregion

    }
}
