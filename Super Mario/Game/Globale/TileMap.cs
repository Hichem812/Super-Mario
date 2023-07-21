using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TiledSharp;

namespace Super_Mario
{
    internal class TileMap :IDisposable
    {
        #region Fields
        private bool disposed;
        TmxMap map;
        internal TileMapManager tilemapManager;
        Texture2D tileset;
        internal List<BoundingBox> BoundinBoxList;
        internal List<Enemy> EnemyList;
        internal List<Block> Blocks;
        internal Point PlayerStart;
        internal List<Rectangle> DeadList;
        internal Rectangle EndRectangle;
        
        #endregion

        #region Constructor
        public TileMap(byte Stage_Number/*,Color BackGroundColor*/)
        {
            //this.BackGroundColor = BackGroundColor;
            map = new TmxMap(Get_StrMap(Stage_Number));
            tileset = Game1.game.Content.Load<Texture2D>("Images\\Texture\\" + map.Tilesets[0].Name.ToString());
            int tilewidth = map.Tilesets[0].TileWidth;
            int tileHeight = map.Tilesets[0].TileHeight;
            int tilesetTileWith = tileset.Width / tilewidth;

            tilemapManager = new TileMapManager(Game1.game.GraphicsDevice, Game1.spriteBatch, map, tileset, tilesetTileWith, tilewidth, tileHeight/*, BackGroundColor*/,1);
                       
            #region Colision Start End
            this.BoundinBoxList = new List<BoundingBox>();
            this.DeadList = new List<Rectangle>();
            foreach (var o in map.ObjectGroups["Colisions"].Objects)
            {
                if (o.Name == "")
                {
                    this.BoundinBoxList.Add(new BoundingBox(
                        new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height)));
                }
                else if(o.Name == "Dead")
                {
                    this.DeadList.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
                }
                else if (o.Name == "Start")
                {
                    PlayerStart.X = (int)o.X;
                    PlayerStart.Y = (int)o.Y;
                }
                else if (o.Name == "End")
                {
                    EndRectangle = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);
                }
            }
            #endregion
            
            #region Enemy
            this.EnemyList = new List<Enemy>();
            foreach (var en in map.ObjectGroups["EnemyPathWays"].Objects)
            {                
                byte Numb = 1;
                byte.TryParse(en.Type, out Numb);
                Rectangle rect = new Rectangle((int)en.X, (int)en.Y, (int)en.Width, (int)en.Height);
                if (en.Name== "KoopaTroopa")
                {                    
                    EnemyKoopaTroopa enemy = new EnemyKoopaTroopa(rect, this.EnemyList);                   
                        this.EnemyList.Add(enemy);
                    if (Numb > 1)
                    {                          
                        int width = rect.Width - enemy.Width;
                        int distance = width / Numb;
                        for (int i = 1; i < Numb; i++)
                        {                            
                            EnemyKoopaTroopa koopa = new EnemyKoopaTroopa(rect, this.EnemyList);
                            koopa.Position = new Vector2(koopa.Position.X + (distance * (i + 1)), koopa.Position.Y);
                            this.EnemyList.Add(koopa);
                        }
                    }                    
                }
                else if (en.Name == "EnemyChampignon")
                {
                    EnemyChampignon enemy = new EnemyChampignon(rect);
                    this.EnemyList.Add(enemy);
                    if (Numb >1)
                    {
                        int width = rect.Width - enemy.Width;
                        int distance = width / Numb;
                        for (int i = 1; i < Numb; i++)
                        {
                            EnemyChampignon champignon = new EnemyChampignon(rect);
                            champignon.Position = new Vector2(champignon.Position.X + (distance * (i + 1)), champignon.Position.Y);
                            this.EnemyList.Add(champignon);
                        }
                    }
                }
            }
            #endregion

            this.Blocks = new List<Block>();
            
        }
        #endregion

        #region Propertis

        #endregion

        #region Methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                this.map.Dispose();
                this.tilemapManager.Dispose();
                //this.tileset.Dispose();
                
                foreach (BoundingBox item in this.BoundinBoxList)
                    item.Dispose();
                this.BoundinBoxList.Clear();
                this.BoundinBoxList = null;

                foreach (Enemy item in this.EnemyList)
                    item.Dispose();
                this.EnemyList.Clear();
                this.EnemyList = null;

                foreach (Block item in this.Blocks)
                    item.Dispose();
                this.Blocks.Clear();
                this.Blocks = null;
                
            }

            disposed = true;

        }

        string Get_StrMap(byte Stage_Number)
        {
            if (Stage_Number < 10)
                return "Content\\Mapes\\Mape-0" + Stage_Number + ".tmx";

            else
                return "Content\\Mapes\\Mape-" + Stage_Number + ".tmx";     
            
        }

        public void GetItems(ref List<Item> items)
        {
            for (var i = 0; i < map.TileLayers.Count; i++)
            {
                if (map.TileLayers[i].Name == "Dynamic")
                {
                    int Width = map.Width;
                    int TileWidth = map.TileWidth;
                    int TileHeight = map.TileHeight;
                    Dynamic LayerDynamic = new Dynamic(items, this.BoundinBoxList, Blocks);
                    LayerDynamic.GetItems(map.TileLayers[i].Tiles, Width, TileWidth, TileHeight);
                    
                    //for (int j = 0; j < map.TileLayers[i].Tiles.Count; j++)
                    //{
                    //    if (map.TileLayers[i].Tiles[j].Gid != 0)
                    //    {
                    //        float x = (j % map.Width) * map.TileWidth;
                    //        float y = (float)Math.Floor(j / (double)map.Width) * map.TileHeight;
                    //        items.Add(new Coin ((int)x, (int)y));
                    //    }
                    //}
                    //break;
                }
            }           
        }
        
        #endregion

        #region Update & Draw

        public void Draw (SpriteBatch spriteBatch,GameTime gameTime)
        {
            this.tilemapManager.Draw(spriteBatch);
        }
               
        #endregion
    }
}
