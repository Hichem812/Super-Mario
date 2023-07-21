using Apos.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using MyLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    public class DetailsTable
    {
        #region Fields
        IMGUI ui;
        Texture2D _coinIcon;
        #endregion

        #region Constructor
        public DetailsTable()
        {
            this.ui = Game1.UI;
                       
            this._coinIcon = Game1.game.Content.Load<Texture2D>("Images\\Texture\\Coin");
        }

        #endregion

        #region Propertise

        #endregion

        #region Methods
        
        #endregion

        #region Update & Draw
        public void UpdateGui(GameTime gameTime)
        {
            GuiHelper.UpdateSetup(gameTime);
            ui.UpdateAll(gameTime);
        }
        internal void InTheGame(GameTime gametime,Player player)
        {
            UpdateGui(gametime);

            TextureRegion2D tes = new TextureRegion2D(_coinIcon);

            
            Icon.Put(tes).XY = new Vector2(10, 5);
            Label.Put("X " + player.Coin + "  Life: " + player.Life + "  Score: " + player.Score,20,Color.Black)
                .XY = new Vector2(40, 0);
            Label.Put(BackGround.str,15,Color.Black).XY= new Vector2(0,30);
            Panel.Push().XY = new Vector2(0, 0);

            //Panel.Push().XY = new Vector2(screenWidth / 2 - 100, screenHeight / 2);

            //Panel.Push().XY = new Vector2(0, 0);
            //Label.Put("Score: " + player.Score + " Health: " + player.health + " s: " + player.Coin, 15);
            Panel.Pop();
        }
        public void YouWon(GameTime gametime)
        {
            UpdateGui(gametime);

            Panel.Push().XY = new Vector2(Settings.GameWidth / 2 - 100, Settings.GameHeight/ 2 - 10);
            Label.Put("You Won!");
            Panel.Pop();
        }
        public void GameOver(GameTime gameTime)
        {
            UpdateGui(gameTime);

            Panel.Push().XY = new Vector2(Settings.GameWidth / 2 - 100, Settings.GameHeight / 2 - 10);
            Label.Put("Game Over");
            Panel.Pop();
        }
        public void Draw(SpriteBatch spritebatche, GameTime gameTime)
        {
           
        }
        #endregion
    }
}
