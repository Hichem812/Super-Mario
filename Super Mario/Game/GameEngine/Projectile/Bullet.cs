using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class Bullet : Projectile
    {
        #region Fields
        
        float Energey;
        float JumpSpeed;
        #endregion

        #region Constructor
        public Bullet(float Speed, Rectangle PlayerHitbox) : 
            base(GetAnimation(), GetPosition(Speed, PlayerHitbox),Speed)
        {
            this.Energey = 150;
            this.JumpSpeed = -5f;
        }
        #endregion

        #region Propertise
        Animation animation { get { return (Animation)this.sprite; } }
        #endregion

        #region Static Methods
        static Animation GetAnimation()
        {
            Animation anim = new Animation(Game1.game.Content.Load<Texture2D>("Images\\Entitys\\Mario\\Bullet")
                ,4,false, 100);
            return anim;
        }
        static Vector2 GetPosition(float Speed, Rectangle PlayerHitbox)
        {       
            int X;
            int Y = PlayerHitbox.Y + (PlayerHitbox.Height / 2) - 9;

            if (Speed > 0)
                X = PlayerHitbox.Right;
            else if (Speed < 0)
                X = PlayerHitbox.Left - 9;
            else throw new Exception("Speed Value Incorect !");

            return new Vector2 (X, Y);
        }
        #endregion

        #region Methods
        internal override bool hasHit(Rectangle Hitbox)
        {
            if (this.Hitbox.Intersects(Hitbox))
            {
                this.Destroy();
                return true;
            }
            else return false;
        }
        #endregion

        #region Update & Draw

        public void Update(GameTime gameTime, List<Enemy> EnemyList,short PlayerScore,List<BoundingBox> BoundinBoxList)
        {
            Update(gameTime);
            animation.Update(gameTime);
            if (this.Energey <= 0)
            {
                this.Destroy();
                return;
            }

            foreach (var Entity in new List<Enemy>(EnemyList))
            {
                if (this.hasHit(Entity.Hitbox))
                {
                    //EnemyList.Remove(Entity);
                    Entity.Destroy();
                    PlayerScore += 10;
                    break;
                }

            }
            foreach (BoundingBox bound in BoundinBoxList)
            {
                if (bound.Intersects(this.Hitbox))
                {
                    this.Destroy();
                    break;
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.Energey--;
            
            if (this.JumpSpeed >= 8)
                this.JumpSpeed = -8.5f;

            this.JumpSpeed += 30 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.Position = new Vector2(this.Position.X, this.Position.Y+this.JumpSpeed);
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            this.animation.Draw(spriteBatch, this.Position,SpriteEffects.None,0.13f);
        }
        #endregion

    }
}
