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
        int backGroundWidth;
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
            this.spriteBackGrounds = GetSprites(Stage_Number);

            this.backGroundWidth = spriteBackGrounds[0, 0].Width;// you can change after exp(make a methods or som thing

            this.positions = GetPositions(Stage_Number,this.backGroundWidth);                      
           
            this._color = GetColor(Stage_Number);
            this.camera = camera;

            this.X = this.camera.Translation.X;
            this.Layers =(byte) this.spriteBackGrounds.GetLength(0) ;
            this.Mouvment = new float[this.Layers];
            this.Calc = new float[this.Layers];
            
            this.Speeds = new float[this.Layers];
            this.Speeds[0] = 0.7f;
            this.Speeds[1] = 0.5f;
            this.Speeds[2] = 0.4f;
            this.Speeds[3] = 0.3f;

            this._initMouv = init;
        }
        #endregion

        #region Propertise
        public Color color { get { return this._color; } set { this._color = value; } }
        #endregion

        #region Methods
        private Vector2[,] GetPositions(byte Stage_Number,int backGroundWidth)
        {
            switch (Stage_Number)
            {
                case 1:
                    Vector2 v1_1 = new Vector2(-backGroundWidth, 1700);
                    Vector2 v1_2 = new Vector2(0, 1700);
                    Vector2 v1_3 = new Vector2(backGroundWidth, 1700);
                    Vector2 v2_1 = new Vector2(-backGroundWidth, 1550);
                    Vector2 v2_2 = new Vector2(0, 1550);
                    Vector2 v2_3 = new Vector2(backGroundWidth, 1550);
                    Vector2 v3_1 = new Vector2(500- backGroundWidth, 1470);
                    Vector2 v3_2 = new Vector2(500, 1470);
                    Vector2 v3_3 = new Vector2(backGroundWidth+500, 1470);
                    Vector2 v4_1 = new Vector2(800 - backGroundWidth, 1420);
                    Vector2 v4_2 = new Vector2(800, 1420);
                    Vector2 v4_3 = new Vector2(backGroundWidth + 800, 1420);
                    Vector2[,] tab = {
                    {v1_1,v1_2,v1_3 },
                    {v2_1,v2_2,v2_3 },
                    {v3_1,v3_2,v3_3 },
                    {v4_1,v4_2,v4_3 } };
                    return tab;

                default:
                    throw new Exception("Stage Not Found");
            }
        }
        private Sprite[,] GetSprites(byte Stage_Number)
        {
            switch (Stage_Number)
            {
                case 1:                    
                    Sprite sp1 = new Sprite(Game1.game.Content.Load<Texture2D>("Images\\Background\\BackGroundLevel1-1"));
                    Sprite sp2 = new Sprite(Game1.game.Content.Load<Texture2D>("Images\\Background\\BackGroundLevel1-2"));
                    Sprite sp3_1 = new Sprite(Game1.game.Content.Load<Texture2D>("Images\\Background\\BackGroundLevel1-3"));
                    Sprite sp3_2 = new Sprite(Game1.game.Content.Load<Texture2D>("Images\\Background\\BackGroundLevel1-4"));
                    Sprite sp3_3 = new Sprite(Game1.game.Content.Load<Texture2D>("Images\\Background\\BackGroundLevel1-5"));
                    Sprite sp4_1 = new Sprite(Game1.game.Content.Load<Texture2D>("Images\\Background\\BackGroundLevel1-6"));
                    Sprite sp4_2 = new Sprite(Game1.game.Content.Load<Texture2D>("Images\\Background\\BackGroundLevel1-7"));
                    Sprite sp4_3 = new Sprite(Game1.game.Content.Load<Texture2D>("Images\\Background\\BackGroundLevel1-8"));

                    Sprite[,] tab = {
                        { sp1, sp1,sp1 },
                        { sp2, sp2,sp2 },
                        { sp3_1, sp3_2,sp3_3},
                        { sp4_1, sp4_2,sp4_3}};
                    return tab;
                   

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
        internal static string str;
        public void Update(bool? ToRight)
        {
            str = "";
            this._initMouv();
            float dif = this.X - this.camera.Translation.X;//diferance betwine the olde positin camera and the new position
                     // speed of camera 
            for (int i = 0; i < this.Speeds.Length; i++) // speeds of all layers
                this.Calc[i]=(int)(dif *this.Speeds[i]);// calc = diferance + more speed

            for (int I = 0; I < this.positions.GetLength(0); I++) { 
                for (int i = 0; i < this.positions.GetLength(1); i++)
                {
                    this.positions[I, i].X -= this.Calc[I];//arreter ici problem sur les positions aprer le game over
                    str += "L" + (I+1) + "_" + i + " " + this.positions[I, i].X;
                }
                str += "\r\n";
            }
            for (int i = 0; i < this.Mouvment.Length; i++)            
                this.Mouvment[i] -= (this.Calc[i] + dif);
            
            for (int I = 0; I < this.Mouvment.Length; I++)
            {
                if (ToRight.HasValue)// the button of direction is preesed
                {
                    if (ToRight.Value)// right button preesed
                    {
                        if (this.Mouvment[I] <= -this.backGroundWidth)
                        {
                            int indexMin, indexMax;
                            GetMinMax(I,out indexMin, out indexMax);

                            this.positions[I, indexMin].X = this.positions[I, indexMax].X + this.backGroundWidth;
                            this.Mouvment[I] += this.backGroundWidth;
                        }
                    }
                    else //if (!ToRight.Value) // left button is presed
                    {
                        if (this.Mouvment[I] >= this.backGroundWidth)
                        {
                            int indexMin, indexMax;
                            GetMinMax(I,out indexMin, out indexMax);

                            this.positions[I, indexMax].X = this.positions[I, indexMin].X - this.backGroundWidth;
                            this.Mouvment[I] -= backGroundWidth;
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
            float Layer = 0.1f;
            for (int i = 0; i < this.spriteBackGrounds.GetLength(0); i++)
            {
                for (int I = 0; I < this.spriteBackGrounds.GetLength(1); I++)
                    spriteBackGrounds[i, I].Draw(spriteBatche, this.positions[i, I], SpriteEffects.None, Layer);

                Layer -= 0.01f;
            }
        }

        #endregion
    }
}
