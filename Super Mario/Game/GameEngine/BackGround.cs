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
        readonly float[] Speeds;
        private Init _initMouv;
        byte Layers;
        #endregion

        #region Constructor
        public BackGround(byte Stage_Number, Camera camera)
        {
            Sprite[] BackGroundsTab = GetSprite1(Stage_Number);
            Vector2[] PositionsTab = GetPositions(Stage_Number);

            this.spriteBackGrounds = new Sprite[2, 3];
            this.positions = new Vector2[2, 3];

            this.spriteBackGrounds[0, 0] = BackGroundsTab[0];
            this.spriteBackGrounds[0, 1] = BackGroundsTab[0];
            this.spriteBackGrounds[0, 2] = BackGroundsTab[0];
            this.spriteBackGrounds[1,0] = BackGroundsTab[1];
            this.spriteBackGrounds[1,1] = BackGroundsTab[1];
            this.spriteBackGrounds[1,2]= BackGroundsTab[1];

            this.positions[0, 0] = PositionsTab[0];
            this.positions[0, 1] = this.positions[0, 0];
            this.positions[0, 0].X = -this.spriteBackGrounds[0, 0].Width;
            this.positions[0, 2] = this.positions[0, 1];
            this.positions[0, 2].X += this.spriteBackGrounds[0, 0].Width;
            
            this.positions[1,0] = PositionsTab[1];
            this.positions[1, 1] = this.positions[1, 0];
            this.positions[1, 0].X = -this.spriteBackGrounds[1, 0].Width;
            this.positions[1, 2] = this.positions[1, 1];
            this.positions[1, 2].X += this.spriteBackGrounds[1, 0].Width;

            this._color = GetColor(Stage_Number);
            this.camera = camera;

            this.X = this.camera.Translation.X;
            this.Layers = 2;
            this.Mouvment = new float[this.Layers];// this.positions[0].X;
            this.Calc = new float[this.Layers];
            
            this.Speeds = new float[this.Layers];
            this.Speeds[0] = 0.4f;
            this.Speeds[1] = 0.3f;

            this._initMouv = init;
        }
        #endregion

        #region Propertise
        public Color color { get { return this._color; } set { this._color = value; } }
        #endregion

        #region Methods
        private Vector2[] GetPositions(byte Stage_Number)
        {
            switch (Stage_Number)
            {
                case 1:
                    Vector2 v1 = new Vector2(0, 1700);
                    Vector2 v2 = new Vector2(0, 1550);
                    Vector2[] tab = { v1, v2 };
                    return tab;

                default:
                    throw new Exception("Stage Not Found");
            }
        }
        private Sprite[] GetSprite1(byte Stage_Number)
        {
            switch (Stage_Number)
            {
                case 1:       
                    Sprite sp1 = new Sprite(Game1.game.Content.Load<Texture2D>("Images\\Background\\BackGroundLevel1-1"));
                    Sprite sp2 = new Sprite(Game1.game.Content.Load<Texture2D>("Images\\Background\\BackGroundLevel1-2"));
                    Sprite[] tab = { sp1, sp2 };
                    return tab;
                    //return new Sprite(Game1.game.Content.Load<Texture2D>("Images\\Background\\BackGroundLevel1-1"));

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
                this.spriteBackGrounds = null;
                this.positions = null;
                this.Mouvment = null;
                this.Calc = null;                
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
            float dif = this.X - this.camera.Translation.X;//diferance betwine the olde positin camera and the new position
                     // speed of camera 
            for (int i = 0; i < this.Speeds.Length; i++)
                this.Calc[i]=(int)(dif *this.Speeds[i]);// calc = diferance + more speed

            for (int I = 0; I < this.positions.GetLength(0); I++)            
                for (int i = 0; i < this.positions.GetLength(1); i++)
                    this.positions[I, i].X -= this.Calc[I];
                        
            //this.positions[0].X -= calc;
            //this.positions[1].X -= calc;
            //this.positions[2].X -= calc;

            //this.Mouvment[0] -= (this.Calc[0] + dif);
            for (int i = 0; i < this.Mouvment.Length; i++)            
                this.Mouvment[i] -= (this.Calc[i] + dif);
            

            for (int I = 0; I < this.Mouvment.Length; I++)
            {
                if (ToRight.HasValue)// the button of direction is preesed
                {
                    if (ToRight.Value)// right button preesed
                    {
                        if (this.Mouvment[I] <= -this.spriteBackGrounds[I, 0].Width)
                        {
                            int indexMin, indexMax;
                            GetMinMax(I,out indexMin, out indexMax);

                            this.positions[I, indexMin].X = this.positions[I, indexMax].X + this.spriteBackGrounds[I, indexMax].Width;
                            this.Mouvment[I] += this.spriteBackGrounds[I, 0].Width;
                        }
                    }
                    else //if (!ToRight.Value) // left button is presed
                    {
                        if (this.Mouvment[I] >= this.spriteBackGrounds[I, 0].Width)
                        {
                            int indexMin, indexMax;
                            GetMinMax(I,out indexMin, out indexMax);

                            this.positions[I, indexMax].X = this.positions[I, indexMin].X - this.spriteBackGrounds[I, indexMax].Width;
                            this.Mouvment[I] -= this.spriteBackGrounds[I, 0].Width;
                        }
                    }
                }

            }

            this.X = camera.Translation.X;
        }
        private void GetMinMax(int Layer, out int indexMin,out int indexMax)
        {
            indexMin = 0;
            indexMax = 0;
            
            for (int i = 0; i < this.positions.GetLength(1); i++)
            {
                if (this.positions[Layer, i].X < this.positions[Layer, indexMin].X) indexMin = i;
                else if (this.positions[Layer, i].X > this.positions[Layer, indexMax].X) indexMax = i;
            }
        }
        public void Draw(SpriteBatch spriteBatche)
        {
            
                for (int I = 0; I < this.spriteBackGrounds.GetLength(1); I++)
                    spriteBackGrounds[0, I].Draw(spriteBatche, this.positions[0, I], SpriteEffects.None, 0.105f);

            for (int I = 0; I < this.spriteBackGrounds.GetLength(1); I++)
                spriteBackGrounds[1, I].Draw(spriteBatche, this.positions[1, I], SpriteEffects.None, 0.10f);



            //for (int i = 0; i < this.spriteBackGrounds.Length; i++)
            //{
            //    spriteBackGrounds[0,i].Draw(spriteBatche, this.positions[0,i], SpriteEffects.None, 0.10f);
            //}

        }

        #endregion
    }
}
