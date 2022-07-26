using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class brickBroken
    {
        #region Fields
        private Rectangle[] Rects;
        private Vector2[] Positions;
        private Texture2D texture;
        private byte phase;
        private SByte mouv;
        private short Timer;
        private BlockBrick brick;
        private float Rotation;
        private Vector2 Origin;
        #endregion

        #region Constructor
        internal brickBroken(Vector2 Position,BlockBrick brick)
        {           
            this.Positions = new Vector2[4];
            for (int i = 0; i < this.Positions.Length; i++)
                this.Positions[i] = new Vector2(Position.X+90,Position.Y);
            
            this.texture = Game1.game.Content.Load<Texture2D>("Images\\Texture\\Brick Broken");
            
            this.Rects    = new Rectangle[4];
            this.Rects[0] = new Rectangle(0,0, 64, 64);
            this.Rects[1] = new Rectangle(64, 0, 64, 64);
            this.Rects[2] = new Rectangle(64, 0, 64, 64);
            this.Rects[3] = new Rectangle(128, 0, 64, 64);

            this.phase = 1;
            this.Timer = 0;
            this.mouv = 17;
            this.brick = brick;
            this.Rotation = 0f;
            this.Origin = new Vector2(96, 16);
        }
        #endregion

        #region Propertise

        #endregion

        #region Methods
        private void MouveUp(GameTime gameTime)
        {
            this.Timer += (short)(gameTime.ElapsedGameTime.Milliseconds);
            for (int i = 0; i < Positions.Length; i++)            
                this.Positions[i].Y -= 15;
            
            if (this.Timer >= 75)
            {
                this.Timer = 0;
                this.phase  ++;
            }           
                            
        }
        private void Separe(GameTime gameTime)
        {
            this.Timer += (short)(gameTime.ElapsedGameTime.Milliseconds);
            this.Positions[0] = new Vector2(this.Positions[0].X += 5, this.Positions[0].Y +=5);
            this.Positions[1] = new Vector2(this.Positions[1].X += 5, this.Positions[1].Y += 5);
            this.Positions[2] = new Vector2(this.Positions[2].X -= 5, this.Positions[2].Y -= 5);
            this.Positions[3] = new Vector2(this.Positions[3].X -= 5, this.Positions[3].Y -= 5);
           
            if (this.Timer >= 50)
            {
                this.Timer = 0;
                this.phase++;
            }
        }
        private void Throwing(GameTime gameTime)
        {
            //this.Timer += (short)(gameTime.ElapsedGameTime.Milliseconds);

            this.Positions[0] = new Vector2(this.Positions[0].X += 4, this.Positions[0].Y -= mouv);
            this.Positions[1] = new Vector2(this.Positions[1].X += 2, this.Positions[1].Y -= mouv);
            this.Positions[2] = new Vector2(this.Positions[2].X -= 4, this.Positions[2].Y -= mouv);
            this.Positions[3] = new Vector2(this.Positions[3].X -= 2, this.Positions[3].Y -= mouv);

            this.mouv--;

            //if (this.Timer >= 1000)
            //{
            //    this.Timer = 0;
            //    this.phase++;
            //}
        }
        #endregion

        #region Update & Draw
        internal void Update(GameTime gameTime)
        {
            this.Positions[0] = new Vector2(this.Positions[0].X += 3, this.Positions[0].Y -= mouv);
            this.Positions[1] = new Vector2(this.Positions[1].X += 1, this.Positions[1].Y -= mouv);
            this.Positions[2] = new Vector2(this.Positions[2].X -= 3, this.Positions[2].Y -= mouv);
            this.Positions[3] = new Vector2(this.Positions[3].X -= 1, this.Positions[3].Y -= mouv);

            this.mouv--;
            if (this.mouv <=-35)
            {
                this.brick.Destroy();
            }
            //switch (phase)
            //{
            //    case 1:
            //    //    MouveUp(gameTime);
            //    //    break;

            //    //case 2:                    
            //    //    Separe(gameTime);
            //    //    break;

            //    //case 3:
            //        Throwing(gameTime);
            //        break;

            //    default:
            //        break;
            //}
        }
        internal void Draw (SpriteBatch spriteBatch)
        {//spriteBatch.Draw(this.texture, this.destinationRectangle, null, this.color, this.Rotation, this.Origin, this.IMGoriantation, 0f);    
            for (int i = 0; i < this.Rects.Length; i++)
                spriteBatch.Draw(this.texture, this.Positions[i], this.Rects[i], Color.White, this.Rotation += 0.005f,this.Origin,1,SpriteEffects.None,0);
        }
        #endregion
    }
}
