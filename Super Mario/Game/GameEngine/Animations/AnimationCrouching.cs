using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class AnimationCrouching : Sprite
    {
        delegate void CrouchingUpdate(GameTime gameTime);
        #region Fields
        CrouchingUpdate UpdateC;
        Texture2D[] Frames;
        float MilisocondsParFrams;
        //readonly short SleepTime;
        short /*SleepTimer, */Timer;
        byte CurentFrameIndex;
        //Vector2 Position;
        #endregion

        #region Constructor
        internal AnimationCrouching(Texture2D[] Sprites, float MilisocondsParFrams/*,Vector2 Position*/) : base(Sprites[0])
        {
            this.UpdateC = UpdateAnime;
            this.Frames = Sprites;   
            this.MilisocondsParFrams = MilisocondsParFrams;
            //this.Position = Position;
            this.CurentFrameIndex = 0;
        }
        #endregion

        #region Propertise
        Texture2D CurentFrame
        { 
            get { return this.Frames[CurentFrameIndex]; }
            //set { this.Frames[CurentFrameIndex] = value; }
        }
        public override short Width { get { return (short)this.CurentFrame.Width; } }
        public override short Height { get { return (short)this.CurentFrame.Height; } }
        #endregion

        #region Methods
        internal void InitialazeAnimation()
        {
            this.CurentFrameIndex = 0;
            this.UpdateC = UpdateAnime;
                
        }
        #endregion

        #region Update & Draw
        public void UpdateAnime(GameTime gameTime)
        {
            this.Timer += (short)(gameTime.ElapsedGameTime.Milliseconds);
            if (this.Timer >= this.MilisocondsParFrams)
            {
                this.Timer = 0;
                //this.Position = new Vector2(this.Position.X,this.Position.Y+40);
                    
                this.CurentFrameIndex++;
                if (this.CurentFrameIndex == this.Frames.Length-1)
                {
                    this.UpdateC = UpdateEco;                        
                }
            }
        }
        public void UpdateEco(GameTime gameTime)
        {

        }
        public override void Update(GameTime gameTime)
        {
            UpdateC(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatche, Vector2 Position, SpriteEffects Effects)
        {
            spriteBatche.Draw(this.CurentFrame, Position, null, Color.White, 0f, new Vector2(), 1f, Effects, 1);
        }
        public override void Draw(SpriteBatch spriteBatche, Vector2 Position, Color color, SpriteEffects Effects = SpriteEffects.None)
        {
            spriteBatche.Draw(this.CurentFrame, Position, null, color, 0f, new Vector2(), 1f, Effects, 1);

        }
        #endregion
    }
}
