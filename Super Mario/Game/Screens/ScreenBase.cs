using Apos.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using FontStashSharp;

namespace Super_Mario
{
    abstract class ScreenBase : Screen
    {
        #region Fields
        IMGUI ui;
        #endregion

        #region Constructor
        public ScreenBase(byte Id) : base(Id)
        {
            this.ui = Game1.UI;            
        }
        #endregion

        #region Update & Draw
        public override void Update(GameTime gametime)
        {
            GuiHelper.UpdateSetup(gametime);
            ui.UpdateAll(gametime);
        }
        public override void Draw(SpriteBatch spritebatche, GameTime gameTime)
        {
           
        }
        #endregion



    }
    public static class ScreenId
    {
        public const byte Start = 0;
        public const byte Game = 1;
        //public const byte Menu = 1;
        //public const byte Setting = 2;
        //public const byte ItemsPlayer = 3;
        //public const byte CoffreSafe = 4;
    }
}
