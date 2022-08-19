using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    /// <summary>
    /// this class for manag all game ( the end , gameOver , switche betwine the levels , ...)
    /// </summary>
    public class GameManager
    {
        delegate void update(GameTime gameTime);

        #region Static Fields

        public static ScreensManager screensmanager;
        public static byte Stage = 1;
        public static Color color;
        #endregion

        #region Fields
        update updates;
        Game1 game;
        Player player;
        public DetailsTable detailsTable;
        
        #endregion

        #region Constructor

        public GameManager(Game1 Game)
        {
           this.game = Game;
            //color = Color.Black;
            screensmanager = new ScreensManager();
            screensmanager.Add(new ScreenStart());           
            this.updates = this.Game;
            this.detailsTable = new DetailsTable();            
        }
        #endregion

        #region Propertis       
        #endregion

        #region Methods

        public void YouWon()
        {
            updates = YouWon;             
        }
        internal void GameOver(Player Player)
        {
            this.player = Player;
            updates = GameOver;
        }
        #endregion

        #region Update & Draw

        public void Update(GameTime gameTime)
        {
            Input.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Input.IskeyDown(Keys.Escape))
                game.Exit();

            if (Input.IskeyDown(Keys.LeftAlt) && Input.IsKey(Keys.Enter))    
                Settings.ToggleFullScreen();
                        

            this.updates(gameTime);
        }
        void Game(GameTime gameTime)
        {
            screensmanager.Update(gameTime);
        }
        void YouWon(GameTime gameTime)
        {
            this.detailsTable.YouWon(gameTime);
        }
        void GameOver(GameTime gameTime)
        {
            if (!this.player.IsGameOver)
            {
                this.detailsTable.GameOver(gameTime);
                this.player.Update(gameTime);
            }           
            else
            {
                //((Screen)(GameManager.screensmanager.Get()));

                //Screen i = GameManager.screensmanager.Get();
                //((ScreenGame)i).NewInstance();
                //this.updates = this.Game;

                //ScreenStart start = new ScreenStart();

                GameManager.screensmanager.AddSet(new ScreenStart());
                this.updates = this.Game;
                GameManager.screensmanager.Remouv(ScreenId.Game);

                //Game1.game.InitializeContent();
                //GameManager.screensmanager.Set(ScreenId.Start);
                //Game1.game.initialaze();
            }
                
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            screensmanager.Draw(spriteBatch, gameTime);
        }
        #endregion
    }
}
