using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class ScreensManager
    {

        #region Fields

        List<Screen> Screens;
        byte Selected;

        #endregion


        #region Constructor

        public ScreensManager()
        {
            Screens = new List<Screen>();

        }

        #endregion


        #region Propertis

        public int Count { get { return Screens.Count; } }

        #endregion


        #region Methods
        internal Screen Get()
        {
            for (int i = 0; i < Count; i++)
            {
                if (Screens[i].Id == this.Selected) return Screens[i];
            }
            throw new Exception("Not Found");
        }

        public void AddSet(Screen NewScreen)
        {
            Add(NewScreen);
            Set(NewScreen);

        }
        public void Ecrase(Screen OldeScreen, Screen NewScreen)
        {
            AddSet(NewScreen);
            Remouv(OldeScreen);
        }
        /// <summary>
        /// Ecrase le mem ID
        /// </summary>
        public void Ecrase(Screen NewScreen)
        {            
            Remouv(NewScreen.Id);
            AddSet(NewScreen);            
        }
        public virtual void Set(byte Id) { this.Selected = Id; }
        public virtual void Set(Screen screen) { this.Selected = screen.Id; }
        public void Add(Screen screen /*,game1 g*/) { Screens.Add(screen); CheckIndex();/*screen.init(g)*/ }

        public void Remouv(Screen screen) { Screens.Remove(screen); }
        public void Remouv(byte Id)
        {
            foreach (Screen item in new List<Screen>(this.Screens))
                if (item.Id == Id)
                {
                    item.Dispose();
                    Remouv(item);
                    return;
                }
            throw new Exception("Id introuvable");
        }
        //public void Clear() { Screens.Clear(); }

        void CheckIndex()
        {
            for (int i = Count - 1; i > 0; i--)
            {
                if (Screens[i].Id < Screens[i - 1].Id)
                {
                    Screen item = this.Screens[i];
                    this.Screens.RemoveAt(i);
                    this.Screens.Insert(i - 1, item);
                    i = Count;
                }
            }
        }

        #endregion


        #region Update & Draw

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Screens[i].Id == this.Selected) Screens[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spritebatche,GameTime gameTime)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Screens[i].Id == this.Selected)
                    Screens[i].Draw(spritebatche,gameTime);
            }
        }

        #endregion


    }    
}
