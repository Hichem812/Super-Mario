using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    public abstract class ObjectBase //If I don't need him, I will combine him and GameObject
    {
        #region Fields

        protected Sprite sprite;
        private Vector2 _Position;
        protected bool isDestroyed;
        #endregion

        #region Constructor

        public ObjectBase(Sprite Sprite,Vector2 Position)
        {
            this.sprite = Sprite; 
            this._Position = Position; 
            this.isDestroyed = false;
        }
        #endregion

        #region Propertise
        public virtual Vector2 Position
        {
            get { return this._Position; }
            set { this._Position = value; }
        }
        //public virtual Vector2 Position { get;set; }
        public abstract bool IsDestroyed { get; }
        #endregion

        #region Methods

        public abstract void Destroy();
        public abstract void OnDestroy();
        #endregion

        #region Update & Draw

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch,GameTime gameTime);
        #endregion
    }
}
