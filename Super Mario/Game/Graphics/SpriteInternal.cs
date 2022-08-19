using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class SpriteInternal : Sprite
    {

        #region Fields
        private bool disposed;
        Rectangle Source; 
        //readonly Point_Byte Cell;
        #endregion

        #region Constructor
        internal SpriteInternal(Texture2D Sprite, byte Columns, byte Rows, byte IndexFrameX, byte IndexFrameY) 
            : base(Sprite)
        {
            
            if (Sprite.Width % Columns != 0) throw new Exception("Value Width or Columns is incorrect");
            if (Sprite.Height % Rows != 0) throw new Exception("Value Height Or Rows is incorrect");
            int SourceWidth = Sprite.Width / Columns;
            int SourceHeight = Sprite.Height / Rows;
            
            this.Source = new Rectangle(IndexFrameX * SourceWidth,IndexFrameY * SourceHeight,SourceWidth, SourceHeight);

        }
        #endregion

        #region Propertise
        //internal Point_Byte Frame
        //{
        //    //set { this.Cell = value; }
        //    get { return this.Cell; }
        //}
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
            base.Dispose();
        }
        #endregion

        #region Update & Draw
        public override void Draw(SpriteBatch spriteBatche, Vector2 Position, SpriteEffects Effects, float layerDeft = 0f)
        {
            
            spriteBatche.Draw(this.spriteSheet, Position, this.Source, Color.White, 0f, new Vector2(), 1f, Effects, layerDeft);
        }

        #endregion
    }
}
