using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace MyLibrary
{
    public abstract class Screen :IDisposable
    {
        #region Fields
        byte id;
        private bool disposed;

        #endregion

        #region Constructor
        public Screen(byte Id)
        {
            this.id = Id;
        }
        #endregion

        #region Methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
               
            }

            disposed = true;
            
        }
        #endregion

        #region Propertis
        public byte Id { get { return this.id; } }
        #endregion

        #region Update & Draw
        //public abstract void Init(Game1 game1);
        public abstract void Update(GameTime gametime);
        public abstract void Draw(SpriteBatch spritebatche, GameTime gameTime);
        #endregion

    }
}
