using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Super_Mario;

namespace TiledSharp // i aded this Class
{   
    public class TileMapManager :IDisposable
    {
        #region Fields        
        private bool disposed;
        RenderTarget2D RenderTarger;
        internal int MapHeightPixel;
        internal int MapWidihPixel;
        //private Color BackGroundColor;
        #endregion

        #region Constructor       
        /// <param name="LayerDnotDrawIt">if You gave a layers you showld not Draw it</param>
        public TileMapManager(GraphicsDevice GraphicsDevice, SpriteBatch SpriteBatch, TmxMap map, Texture2D tileset, int tilesetTileswide, int tilewidth, int tileHeight/*,Color BackGroundColor*/, byte LayerDnotDrawIt = 0)
        {
            //this.BackGroundColor = BackGroundColor;

            MapWidihPixel = map.TileWidth * map.Width;
            MapHeightPixel = map.TileHeight * map.Height;
            this.RenderTarger = new RenderTarget2D(GraphicsDevice, MapWidihPixel, MapHeightPixel);
            //RenderTarger = new RenderTarget2D(GraphicsDevice, 2048,1024);


            GraphicsDevice.SetRenderTarget(this.RenderTarger);
            GraphicsDevice.Clear(GameManager.color*0f);//Color.SkyBlue);
            SpriteBatch.Begin();
            //for (var i = map.TileLayers.Count - 1; i >= 0; i--)// Layers signifie Couches
            for (var i = 0; i < map.TileLayers.Count - LayerDnotDrawIt/*-1 pour eviter les s*/; i++)
            {
                //if (map.TileLayers[i].Name == "Coin")
                //    continue; iviter l'index dernier
                for (int j = 0; j < map.TileLayers[i].Tiles.Count; j++)
                {
                    int gid = map.TileLayers[i].Tiles[j].Gid;

                    if (gid == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTileswide;
                        //int row = (int)Math.Floor((double)tileFrame / tilesetTileswide);
                        int row = tileFrame / tilesetTileswide;
                        float x = (j % map.Width) * map.TileWidth;
                        float y = (float)Math.Floor(j / (double)map.Width) * map.TileHeight;
                        Rectangle tilesetRec = new Rectangle((tilewidth) * column, (tileHeight) * row, tilewidth, tileHeight);
                        SpriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tilewidth, tileHeight), tilesetRec, Color.White);
                    }

                }
            }
            SpriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
            
        }
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
                this.RenderTarger.Dispose();
            }

            disposed = true;

        }
        #endregion

        #region Updat & Draw
        public void Draw(SpriteBatch spritebatch)
        {   
            spritebatch.Draw(this.RenderTarger,new Rectangle(0,0,this.RenderTarger.Width,this.RenderTarger.Height)
                ,null ,Color.White,0f,Vector2.Zero/*Vector2.Zero*/,SpriteEffects.None,0.11f);

            //spriteBatche.Draw(spriteSheet, Position, rect, color, 0f, new Vector2(), 1f, effect, layerDeft);
        }
       
        #endregion
    }
}
