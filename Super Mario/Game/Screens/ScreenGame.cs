﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using TiledSharp;

namespace Super_Mario
{
    internal class ScreenGame : ScreenBase
    {
        #region Static Fields
        //public static Vector2 PlayerPosition;
        #endregion

        #region Fields
        Player player;
        DetailsTable detailsTable;        
        TileMap tileMap;
        List<Item> Items;
        List<BoundingBox> BoundinBoxList;
        List<Enemy> EnemyList;
        List<Bullet> bullets;
        List<Block> Blocks;
        Rectangle EndRectangle;

        #endregion

        #region Constructor
        public ScreenGame() : base(ScreenId.Game)
        {
            this.detailsTable = Game1.game.gameManager.detailsTable;
            this.tileMap = new TileMap(GameManager.Stage);

            this.Items = new List<Item>();
            this.tileMap.GetItems(ref this.Items);
            this.BoundinBoxList = tileMap.BoundinBoxList;
            this.Blocks = tileMap.Blocks;
            this.EnemyList = tileMap.EnemyList;
            //this.EnemyList = new List<Enemy>();

            this.bullets = new List<Bullet>();
            this.EndRectangle = tileMap.EndRectangle;
            this.player = new Player(this.tileMap.PlayerStart, bullets, BoundinBoxList);
            this.EnemyList = this.tileMap.EnemyList;
            foreach (Enemy enemy in this.EnemyList)            
                enemy.SetPlayer(this.player);
            
        }
        #endregion
        
        #region Propertise
         

        #endregion

        #region Update & Draw
        public override void Update(GameTime gametime)
        {
            Game1.game.transformMatrix = Game1.game.camera.Update(this.player.Hitbox, tileMap.tilemapManager.MapWidihPixel, tileMap.tilemapManager.MapHeightPixel);
            foreach (var item in new List<Item>(this.Items))
            {
                item.Update(gametime,this.player);
                if (item.IsDestroyed)this.Items.Remove(item);
            }
            this.detailsTable.InTheGame(gametime, player);
                      
            player.Update(gametime);
            foreach (var item in new List<Block> (this.Blocks))
            {
                item.Update(gametime);
                if (item.IsDestroyed)this.Blocks.Remove(item);
            }
            foreach (var bul in new List<Bullet>(this.bullets))
            {
                bul.Update(gametime,EnemyList,player.Score, BoundinBoxList); 
                if (bul.IsDestroyed)this.bullets.Remove(bul);
            }
               
             foreach (Enemy enemy in new List<Enemy>( this.EnemyList))
            {
                enemy.Update(gametime);
                if (enemy.IsDestroyed)this.EnemyList.Remove(enemy);
            }

            if (this.player.Hitbox.Intersects(EndRectangle))            
                Game1.game.gameManager.YouWon();
            
            if (this.player.powerStateType == PowerStateType.Lous)            
                Game1.game.gameManager.GameOver(this.player);
            
            base.Update(gametime);
        }

        public override void Draw(SpriteBatch spritebatche, GameTime gameTime)
        {
            base.Draw(spritebatche, gameTime);

            this.tileMap.Draw(spritebatche, gameTime);

            foreach (var item in this.Blocks)            
                item.Draw(spritebatche,gameTime);
            
            foreach (var item in this.Items)            
                item.Draw(spritebatche, gameTime);
            
            foreach (Enemy enemy in this.EnemyList)            
                 enemy.Draw(spritebatche,gameTime);
            
            foreach (var bul in this.bullets)            
                bul.Draw(spritebatche, gameTime);
            
            player.Draw(spritebatche, gameTime);           
        }
    }

    #endregion

}
