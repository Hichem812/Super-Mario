using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal abstract class Projectile : GameObject
    {
        
        #region Fields
        protected float speed;// speed and direction
        #endregion

        #region Constructor
        protected Projectile(Sprite sprite, Vector2 Position,float Speed) :
            base( sprite, Position)

        {
            this.speed = Speed;
        }
        #endregion

        #region Propertise

        #endregion



        #region Methods
        internal abstract bool hasHit(Rectangle Hitbox);
        #endregion

        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            this.Position = new Vector2(this.Position.X + this.speed,this.Position.Y) ;
        }

        #endregion
    }
}
