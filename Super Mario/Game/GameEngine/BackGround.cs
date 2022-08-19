using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using MyLibrary;

namespace Super_Mario
{
    internal class BackGround : IDisposable
    {
        delegate void Init(); 
        #region Fields
        private bool disposed;
        private Color _color;
        Sprite[,] spriteBackGrounds;
        Vector2[,] positions;
        Camera camera;
        float X;
        float[] Mouvment;
        float[] Calc;
        private Init _initMouv; 
        #endregion

        #region Constructor
        public BackGround(byte Stage_Number,Camera camera)
        {
            this.spriteBackGrounds = new Sprite[1,3];

            //this.spriteBackGrounds = new Sprite[3];
            this.spriteBackGrounds[0,0] = GetSprite1(Stage_Number);
            this.spriteBackGrounds[0,1] = this.spriteBackGrounds[0,0];
            this.spriteBackGrounds[0,2] = this.spriteBackGrounds[0,0];
            this.positions = new Vector2[1,3];
            this.positions[0,0] = GetPositions(Stage_Number);
            this.positions[0,1] = this.positions[0,0];
            this.positions[0,0].X = -this.spriteBackGrounds[0,0].Width;
            this.positions[0,2] = this.positions[0,1];
            this.positions[0,2].X += this.spriteBackGrounds[0,0].Width;
            this._color = GetColor(Stage_Number);
            this.camera = camera;
           
            this.X = this.camera.Translation.X;
            this.Mouvment = new float[1];// this.positions[0].X;
            this.Calc = new float[this.Mouvment.Length]; 
            this._initMouv = init;
        }
        #endregion

        #region Propertise
        public Color color { get { return this._color; } set { this._color = value; } }
        #endregion

        #region Methods
        private Vector2 GetPositions(byte Stage_Number)
        {
            switch (Stage_Number)
            {
                case 1:
                    return new Vector2(0, 1700);

                default:
                    throw new Exception("Stage Not Found");
            }
        }
        private Sprite GetSprite1(byte Stage_Number)
        {
            switch (Stage_Number)
            {
                case 1:                    
                    return new Sprite(Game1.game.Content.Load<Texture2D>("Images\\Background\\BackGroundLevel1-1"));
                        
                default:
                    throw new Exception("Stage Not Found");
            }
        }
        private Color GetColor(byte Stage_Number)
        {
            switch (Stage_Number)
            {
                case 1:
                    return new Color(93, 182, 236);
                    
                default:
                    throw new Exception("Stage Not Found");
                    
            }
        }
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

        #region Update & Draw       
        void init()
        {
            this._initMouv = notInit;
            for (int i = 0; i < this.Mouvment.Length; i++)
            {
                this.Mouvment[i] = -this.camera.Translation.X;
            }
            
        }
        void notInit()
        {

        }
        public void Update(bool? ToRight)
        {
            this._initMouv();
            float dif = this.X - this.camera.Translation.X;
            this.Calc[0] = (int)(dif * 0.3f);

            for (int i = 0; i < this.positions.GetLength(1); i++)
                this.positions[0,i].X -= this.Calc[0];

            //this.positions[0].X -= calc;
            //this.positions[1].X -= calc;
            //this.positions[2].X -= calc;
            
            this.Mouvment[0] -= (this.Calc[0] + dif);
            for (int I = 0; I < this.Mouvment.Length; I++)
            {
                if (ToRight.HasValue && ToRight.Value)
                {
                    if (this.Mouvment[I] <= -this.spriteBackGrounds[I,0].Width)
                    {
                        int indexMin, indexMax;
                        GetMinMax(out indexMin, out indexMax);

                        this.positions[0,indexMin].X = this.positions[0,indexMax].X + this.spriteBackGrounds[I,indexMax].Width;
                        this.Mouvment[I] += this.spriteBackGrounds[I,0].Width;
                    }
                }
                else if (ToRight.HasValue && !ToRight.Value)
                {
                    if (this.Mouvment[I] >= this.spriteBackGrounds[I,0].Width)
                    {
                        int indexMin, indexMax;
                        GetMinMax(out indexMin, out indexMax);

                        this.positions[0,indexMax].X = this.positions[0,indexMin].X - this.spriteBackGrounds[I,indexMax].Width;
                        this.Mouvment[I] -= this.spriteBackGrounds[I,0].Width;
                    }
                }
            }

            this.X = camera.Translation.X;
        }
        private void GetMinMax(out int indexMin,out int indexMax)
        {
            indexMin = 0;
            indexMax = 0;
            for (int i = 0; i < this.positions.Length; i++)
            {
                if (this.positions[0,i].X < this.positions[0,indexMin].X) indexMin = i;
                else if (this.positions[0,i].X > this.positions[0,indexMax].X) indexMax = i;
            }
        }
        public void Draw(SpriteBatch spriteBatche)
        {
           
            for (int i = 0; i < this.spriteBackGrounds.Length; i++)
            {
                spriteBackGrounds[0,i].Draw(spriteBatche, this.positions[0,i], SpriteEffects.None, 0.10f);
            }
            
        }
      
        #endregion
    }
}
