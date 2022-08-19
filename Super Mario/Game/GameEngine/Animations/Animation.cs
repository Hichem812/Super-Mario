using Apos.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    delegate void UpdateDelegate(GameTime gameTime);
    internal class Animation : Sprite
    {
        #region Fields
        //internal Color color;
        private bool disposed;
        UpdateDelegate update;
        readonly short SleepTime;
        short SleepTimer, Timer;
        byte frames, ColumnCurent,width, height;
        public float MilisocondsParFrams;
        private bool End;
        Rectangle rect;
        //public readonly EntityState State;
        
        #endregion

        #region Constructor    
        /// <summary>
        /// Animation Cruching
        /// </summary>
        /// <param name="spritesheet"></param>
        //public Animation(Texture2D spritesheet) : base(spritesheet)
        //{
        //    this.update = UpdateCruching;
        //    Initialaze(spritesheet, 1, 500);
        //    this.height =105;
        //}
        public Animation(Texture2D spritesheet, byte frames, float MilisocondsParFrams,short SleepTime) : base(spritesheet)
        {
            this.SleepTime = SleepTime;
            this.update = UpdateSleep;
            Initialaze(spritesheet, frames, MilisocondsParFrams);
        }
        public Animation(Texture2D spritesheet, byte frames, bool IsJumping = false, float MilisocondsParFrams=100f):base(spritesheet)  // 19 * 2 /*,byte ColumnBigen,byte ColumnEnd, byte Row = 0*/
        {
            if (IsJumping) this.update= UpdateAndFinish;
            else this.update = UpdateSimple;
            Initialaze(spritesheet, frames, MilisocondsParFrams);
        }
       /* /// <summary>
        /// Animation Fix
        /// </summary>
        //public Animation(Texture2D spritesheet, byte frames, byte ColumnCurent) : base(spritesheet)
        //{
        //    this.MilisocondsParFrams = 0;
        //    this.width = (byte)(spritesheet.Width / frames);
        //    this.height = (byte)spritesheet.Height;
        //    this.Timer = 0;
        //    this.spriteSheet = spritesheet;
        //    this.ColumnCurent = ColumnCurent;
        //    this.rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*//*, this.width, this.height);
        //    this.update = DontUpdate;
        //}*/
         void Initialaze(Texture2D spritesheet, byte frames, float MilisocondsParFrams)
        {            
            this.MilisocondsParFrams = MilisocondsParFrams;
            this.width = (byte)(spritesheet.Width / frames);
            this.height = (byte)spritesheet.Height;
            this.spriteSheet = spritesheet;
            this.frames = frames;
            this.End = false;
            this.Zero();
        }
        internal void Zero()
        {
            this.Timer = 0;
            this.ColumnCurent = 0;
            this.rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
        }
        #endregion

        #region Propertis        
        public byte Frames { get { return this.frames; } }
        public override short Width { get { return this.width; } }
        public override short Height { get { return this.height; } }
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
            base.Dispose(disposing);
        }
        private void InitialazeThrowing()
        {
            this.Timer = 0;
            this.End = false;
            this.ColumnCurent = 0;
            this.rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
            this.update = UpdateAndFinish;
        }
        internal bool TheEnd()
        {
            if (this.End)
            {
                InitialazeThrowing();
                return true;
            }
            return this.End;
        }
        internal void initialazeForJumping()
        {
            this.Timer = 0;
            this.ColumnCurent=0;
            this.rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
            this.update = UpdateAndFinish;
        }
        //public void SetCruchinHeight()
        //{
        //    this.height = 105;
        //}
        #endregion

        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            this.update(gameTime);            
        }
        internal void DontUpdate(GameTime gameTime)
        {
            
        }
        internal void UpdateSleep(GameTime gameTime)
        {           
            if (this.SleepTimer < this.SleepTime)            
                this.SleepTimer += (short)(gameTime.ElapsedGameTime.Milliseconds);
                           
            else
            {                                   
                this.Timer += (short)(gameTime.ElapsedGameTime.Milliseconds);                
                if (this.Timer >= this.MilisocondsParFrams)
                {
                    
                    this.Timer = 0;
                    this.ColumnCurent++;
                    this.rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
                    if (this.ColumnCurent >= this.frames)
                    {
                        this.ColumnCurent = 0;
                        this.rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
                        this.SleepTimer = 0;
                    }                   
                }
            }            
        }
        internal void UpdateAndFinish(GameTime gameTime)
        {
            this.Timer += (short)(gameTime.ElapsedGameTime.Milliseconds);
            //var rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
            if (this.Timer >= this.MilisocondsParFrams)
            {               
                    this.Timer = 0;
                    this.ColumnCurent++;
                    this.rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
                
                if (this.ColumnCurent >= this.frames)
                {
                    this.update = DontUpdate;
                    this.End = true;
                }
               
            }
        }
        internal void UpdateSimple(GameTime gameTime)
        {
            this.Timer += (short)(gameTime.ElapsedGameTime.Milliseconds);
            //var rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
            if (this.Timer >= this.MilisocondsParFrams)
            {
                this.Timer = 0;
                this.ColumnCurent++;
                this.rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
                if (this.ColumnCurent >= this.frames)
                {
                    this.ColumnCurent = 0;
                    this.rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
                }               
            }
        }
        public override void Draw(SpriteBatch spriteBatche, Vector2 Position, SpriteEffects effect = SpriteEffects.None, float layerDeft = 0f)
        {            
            //var rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
            spriteBatche.Draw(spriteSheet, Position, rect,Color.White, 0f, new Vector2(), 1f, effect, layerDeft);
        }
        public override void Draw(SpriteBatch spriteBatche, Vector2 Position, Color color, SpriteEffects effect = SpriteEffects.None, float layerDeft = 0f)
        {
            //var rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
            spriteBatche.Draw(spriteSheet, Position, rect, color, 0f, new Vector2(), 1f, effect, layerDeft);
        }
        #endregion
    }  
}
