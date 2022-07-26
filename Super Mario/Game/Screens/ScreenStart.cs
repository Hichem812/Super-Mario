using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyLibrary;
using Apos.Gui;
using FontStashSharp;

namespace Super_Mario
{
    internal class ScreenStart : ScreenBase
    {
        #region Fields
        

        Vector2 PositonTitel;
        Vector2 PositionButton;
        #endregion

        #region Constructor
        public ScreenStart() : base(ScreenId.Start)
        {            
            this.PositonTitel = new Vector2((Settings.GameWidth / 2) - 80, 10);
            this.PositionButton = new Vector2(PositonTitel.X + 40, PositonTitel.Y + 200);
        }

        #endregion

        #region Methods

        #endregion

        #region Update & Draw       

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);  

            Panel.Push().XY = this.PositonTitel;
            Label.Put("Supar Mario", 20);
            Panel.Pop();

            MenuPanel.Push().XY = this.PositionButton;
            if(Button.Put("New Game", 15).Clicked)
            {
                GameManager.screensmanager.Ecrase(this, new ScreenGame());
            }
            Label.Put("");
            if (Button.Put("Exit", 15).Clicked)
            {
                Game1.game.Exit();
            }
            ;
            MenuPanel.Pop();            
            
        }

        public override void Draw(SpriteBatch spritebatche,GameTime gameTime)
        {
            //base.Draw(spritebatche, gameTime);
        }
        #endregion

    }
}
