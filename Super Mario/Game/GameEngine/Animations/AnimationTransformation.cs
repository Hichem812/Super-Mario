using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class AnimationTransformation : SpriteInternal
    {
        delegate void WitchDraw(SpriteBatch spriteBatche, Vector2 Position, SpriteEffects effect = SpriteEffects.None);
        #region Fields
        private bool disposed;
        SpriteInternal /*FirstSprite,*/ SecondSprite/*, CurentSprite*/;
        short Timer, MilisocondsParFrams;
        bool End;
        WitchDraw witchDraw;
        #endregion

        #region Constructor
        internal AnimationTransformation(
            Texture2D Sprite1, byte Sp1Columns, byte Sp1Rows, byte Sp1IndexFrameX, byte Sp1IndexFrameY,
            Texture2D Sprite2, byte Sp2Columns, byte Sp2Rows, byte Sp2IndexFrameX, byte Sp2IndexFrameY)
            : base(Sprite1, Sp1Columns, Sp1Rows, Sp1IndexFrameX, Sp1IndexFrameY)
        {
            this.SecondSprite = new SpriteInternal(Sprite2, Sp2Columns, Sp2Rows, Sp2IndexFrameX, Sp2IndexFrameY);
            Initialaze();
        }
        //internal AnimationTransformation(SpriteInternal FirstSprite,SpriteInternal SecondSprite)
        //{
        //    this.FirstSprite = FirstSprite;
        //    this.SecondSprite = SecondSprite;
        //    this.CurentSprite = this.FirstSprite;

        //}
        #endregion

        #region Propertise

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
                SecondSprite.Dispose();
            }

            disposed = true;
            base.Dispose(disposing);
        }
        private void Initialaze()
        {
            this.witchDraw = DrawThis;
            this.Timer = 0;
            this.MilisocondsParFrams = 200;
            this.End = false;
        }
        internal bool TheEnd()
        {
            if (this.End)
            {
                Initialaze();
                return true;
            }
            return this.End;
        }
        #endregion

        #region Update & Draw

        public override void Update(GameTime gameTime)
        {
            this.Timer += (short)(gameTime.ElapsedGameTime.Milliseconds);
            //var rect = new Rectangle(this.width * this.ColumnCurent, this.height * 0/*Row*/, this.width, this.height);
            if (this.Timer >= MilisocondsParFrams)
            {
                this.Timer = 0;
                this.MilisocondsParFrams -= 1;

                if (this.witchDraw == DrawThis) this.witchDraw = DrawSecondSprite;
                else this.witchDraw = DrawThis;


                //if (this.MilisocondsParFrams == 100) this.MilisocondsParFrams = 50;
                //else this.MilisocondsParFrams = 100;

                //if (this.CurentSprite.Equals(this)) CurentSprite = SecondSprite;
                //else CurentSprite = this;

                if (this.MilisocondsParFrams <= 195)
                {
                    this.End = true;
                }

            }
           
        }
        private void DrawThis(SpriteBatch spriteBatche, Vector2 Position, SpriteEffects effect = SpriteEffects.None)
        {
            base.Draw(spriteBatche, Position, effect);
        }
        private void DrawSecondSprite(SpriteBatch spriteBatche, Vector2 Position, SpriteEffects effect = SpriteEffects.None)
        {
            this.SecondSprite.Draw(spriteBatche, Position, effect);
        }
        public override void Draw(SpriteBatch spriteBatche, Vector2 Position, SpriteEffects effect = SpriteEffects.None)
        {
            this.witchDraw(spriteBatche, Position, effect);
        }
        public override void Draw(SpriteBatch spriteBatche, Vector2 Position,Color color, SpriteEffects effect = SpriteEffects.None)
        {
            this.witchDraw(spriteBatche, Position, effect);
        }
        #endregion
    }
}
