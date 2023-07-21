using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TiledSharp;

namespace Super_Mario
{
    internal class Dynamic
    {
        #region Fields
        //internal Collection<TmxLayerTile> Tiles { get; private set; }
        List<Item> items;
        Dictionary<string, int> itemDictionary;
        List<Block> Blocks;
        List<BoundingBox> BoundinBoxList;
        #endregion

        #region Constructor
        internal Dynamic(/*Collection<TmxLayerTile> Tiles,*/ List<Item> items, List<BoundingBox> boundinBoxList, List<Block> blocks)
        {
            this.items = items;
            this.itemDictionary = new Dictionary<string, int>();
            this.InitialazeItemDictionary();
            this.BoundinBoxList = boundinBoxList;
            this.Blocks = blocks;
        }
        #endregion

        #region Propertise

        #endregion

        #region Methods

        internal void GetItems(Collection<TmxLayerTile> Tiles, int Width, int TileWidth, int TileHeigh)
        {

            for (int j = 0; j < Tiles.Count; j++)
            {
                if (Tiles[j].Gid != 0)
                {
                    float x = (j % Width) * TileWidth;
                    float y = (float)Math.Floor(j / (double)Width) * TileHeigh;

                    if (Tiles[j].Gid == this.itemDictionary["EmptyBlock"])
                        this.Blocks.Add(new EmptyBlock(this.BoundinBoxList, x, y));

                    if (Tiles[j].Gid == this.itemDictionary["Coin"])                    
                        this.items.Add(new Coin((int)x, (int)y));
                    
                    else if (Tiles[j].Gid == this.itemDictionary["BlockBrick"])                    
                        this.Blocks.Add(new BlockBrick(this.BoundinBoxList, x, y));
                    
                    else if (Tiles[j].Gid == this.itemDictionary["BlockSurpriseCoin"])                    
                        this.Blocks.Add(new BlockSurprise(new Coin(0, 0), items, this.BoundinBoxList, x, y));
                   
                    else if (Tiles[j].Gid == this.itemDictionary["BlockSurpriseChampignon"])
                        this.Blocks.Add(new BlockSurprise(new ItemChampignon(0, 0), items, this.BoundinBoxList, x, y));

                    else if (Tiles[j].Gid == this.itemDictionary["BlockSurpriseFireFlower"])
                        this.Blocks.Add(new BlockSurpriseFire(new ItemChampignon(0, 0), new ItemFireFlower(0, 0), items, this.BoundinBoxList, x, y));
                    
                    else if (Tiles[j].Gid == this.itemDictionary["BlockSurpriseChampignon1Up"])
                        this.Blocks.Add(new BlockSurprise(new ItemChampignon1Up(0, 0), items, this.BoundinBoxList, x, y));


                    else if (Tiles[j].Gid == this.itemDictionary["BlockSupriseInvisibleChampignon1Up"])
                        this.Blocks.Add(new BlockSupriseInvisible(new ItemChampignon1Up(0, 0), items, this.BoundinBoxList, x, y));

                    else if (Tiles[j].Gid == this.itemDictionary["BlockSupriseInvisibleCoin"])
                        this.Blocks.Add(new BlockSupriseInvisible(new Coin(0, 0), items, this.BoundinBoxList, x, y));

                    else if (Tiles[j].Gid == this.itemDictionary["BlockSupriseInvisibleChampignon"])
                        this.Blocks.Add(new BlockSupriseInvisible(new ItemChampignon(0, 0), items, this.BoundinBoxList, x, y));

                    else if (Tiles[j].Gid == this.itemDictionary["BlockSupriseFireInvisible"])
                        this.Blocks.Add(new BlockSupriseFireInvisible(new ItemChampignon(0, 0), new ItemFireFlower(0, 0), items, this.BoundinBoxList, x, y));
                }
            }
        }
        private void InitialazeItemDictionary()
        {
            this.itemDictionary.Add("EmptyBlock", 10);
            this.itemDictionary.Add("Coin", 18);
            this.itemDictionary.Add("BlockBrick", 1);
            this.itemDictionary.Add("BlockSurpriseCoin", 8);
            this.itemDictionary.Add("BlockSurpriseChampignon", 7);
            this.itemDictionary.Add("BlockSurpriseFireFlower", 2);
            this.itemDictionary.Add("BlockSurpriseChampignon1Up", 4);

            this.itemDictionary.Add("BlockSupriseInvisibleChampignon1Up",13 );
            this.itemDictionary.Add("BlockSupriseInvisibleCoin", 17);
            this.itemDictionary.Add("BlockSupriseInvisibleChampignon", 16);
            this.itemDictionary.Add("BlockSupriseFireInvisible", 11);
        }
        #endregion

        #region Update & Draw

        #endregion
    }
}
