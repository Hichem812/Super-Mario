using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using MyLibrary;

namespace MyLibrary
{
    internal class Camera
    {
        #region Fields
       
        public Matrix Transform;
        int Width;
        int Height;
        Rectangle target;
        #endregion

        #region Constructor
        public Camera()
        {
            this.Width = Settings.GameWidth / 2 ;
            this.Height = Settings.GameHeight / 2;
            
        }
        #endregion

        #region Propertise

        #endregion

        #region Methods
        public void InitialazeTarget(Rectangle Player)
        {
            int width = Player.Width*5;
            int height = Player.Height*3;
            target = new Rectangle(Player.Right, Player.Top-Player.Height, width, height);
        }

        //internal void InitialazeMapSize(int MapWidihPixel, int MapHeightPixel, Rectangle HitboxPlayer)
        //{
        //    MaxClampWidth = MapWidihPixel - Width - (HitboxPlayer.Width / 2);
        //    MaxClampHeight = MapHeightPixel - Height - (HitboxPlayer.Height / 2);
        //}
        #endregion

        #region Update & Draw

        public Matrix Update(Rectangle Player, int MapWidihPixel, int MapHeightPixel)
        {
            if (Player.Right > this.target.Right) this.target.X = Player.Right - this.target.Width;
            else if (Player.Left < this.target.Left) this.target.X = Player.Left;

            if (Player.Top < this.target.Top) this.target.Y = Player.Top;
            else if (Player.Bottom > this.target.Bottom) this.target.Y =Player.Y -( this.target.Height - Player.Height);    
            
            target.X = MathHelper.Clamp(target.X, Width, MapWidihPixel - Width-(target.Width/2)/*- 30*//* (int)Settings.ScreenWidth / 2 + 495*/);// clamp pour limiter la camera horizontalemant

            target.Y = MathHelper.Clamp(target.Y, Height, MapHeightPixel-Height-(target.Height/2 )/*Height + 1220*/ /*+ TarbertYValue*/);// clamp pour limeter la camera verticalement

            Vector3 translation = new Vector3(-target.X - target.Width / 2,  // depalacmant de camera an suvent le player et (-) an sanse contre la direction de player 
                                        -target.Y - target.Height / 2, 0);

            Vector3 offset = new Vector3(Settings.GameWidth / 2, Settings.GameHeight / 2, 0);//la taile de l'cran parapor la map


            Transform = Matrix.CreateTranslation(translation) * Matrix.CreateTranslation(offset)/**zoom*/;

            return Transform;
        }
        //public Matrix Update(Rectangle target, int MapWidihPixel, int MapHeightPixel)
        //{
        //    target.X = MathHelper.Clamp(target.X, Width, MapWidihPixel - Width - (target.Width / 2)/*- 30*//* (int)Settings.ScreenWidth / 2 + 495*/);// clamp pour limiter la camera horizontalemant

        //    target.Y = MathHelper.Clamp(target.Y, Height, MapHeightPixel - Height - (target.Height / 2)/*Height + 1220*/ /*+ TarbertYValue*/);// clamp pour limeter la camera verticalement

        //    Vector3 translation = new Vector3(-target.X - target.Width / 2,  // depalacmant de camera an suvent le player et (-) an sanse contre la direction de player 
        //                                -target.Y - target.Height / 2, 0);

        //    Vector3 offset = new Vector3(Settings.GameWidth / 2, Settings.GameHeight / 2, 0);//la taile de l'cran parapor la map


        //    Transform = Matrix.CreateTranslation(translation) * Matrix.CreateTranslation(offset)/**zoom*/;

        //    return Transform;
        //}
        #endregion




    }
}
