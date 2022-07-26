using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Xml.Serialization;
using System.IO;
using Super_Mario;
using Microsoft.Xna.Framework.Graphics;

namespace MyLibrary
{
    public class Settings
    {
        // Static Fields

       
        // La dimension reele du televiseur de la personne        
        public static Vector2 windowsRes;
        
        // La dimension en largeur à laquelle le jeu est développé        
        public static short GameWidth = 1024;
        
        //La dimension en hauteur à laquelle le jeu est développé        
        public static short GameHeight = 640;

        /// <summary>
        /// Cette fonction permet de passer en mode plein ecran
        /// Le paramètre booléen permet de ne pas regarder si on est déjà en fullscreen (On l'utilise au démarrage du jeu)
        /// </summary>
        private static void SetFullScreen(bool FORCED = false)
        {
            if(!FORCED)
                if (IS_FULLSCREEN) return; //deja en full screen

            Settings.IS_FULLSCREEN = true;
            Game1.graphics.IsFullScreen = true;
            Game1.graphics.PreferredBackBufferWidth = (int)windowsRes.X;
            Game1.graphics.PreferredBackBufferHeight = (int)windowsRes.Y;
            Game1.DestinationRec = new Rectangle(0, 0, (int)Settings.windowsRes.X, (int)Settings.windowsRes.Y);

            Game1.graphics.ApplyChanges();
        }

        /// <summary>
        /// Cette fonction permet de passer en mode fenêtré.
        /// Le paramètre booléen permet de ne pas regarder si on est déjà en mode fenetré (On l'utilise au démarrage du jeu)
        /// </summary>
        private static void SetStopFullScreen(bool FORCED = false)
        {
            if (!FORCED)
                if (!IS_FULLSCREEN) return; //deja en mode fenetré

            Settings.IS_FULLSCREEN = false;
            Game1.graphics.IsFullScreen = false;
            Game1.graphics.PreferredBackBufferWidth = Settings.GameWidth;
            Game1.graphics.PreferredBackBufferHeight = Settings.GameHeight;
            Game1.DestinationRec = new Rectangle(0, 0, (int)Settings.GameWidth, (int)Settings.GameHeight);

            Game1.graphics.ApplyChanges();
        }

        /// <summary>
        /// Cette fonction permet de switch entre le mode fullscreen et non fullscreen
        /// </summary>
        public static void ToggleFullScreen()
        {
            if (!IS_FULLSCREEN) 
                SetFullScreen();
            else
                SetStopFullScreen();
        }

        /// <summary>
        /// Cette fonction doit être appelée une seule fois au lancement du jeu.
        /// Permet de donner les donnés de settings
        /// </summary>
        public static void SetOpionsScreen()
        {
            Game1.graphics.HardwareModeSwitch = false;

            try
            {
                DisplayMode currentDisplayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
                Settings.windowsRes = new Vector2((float)currentDisplayMode.TitleSafeArea.Width, (float)currentDisplayMode.TitleSafeArea.Height);
            }
            catch
            {
                Settings.windowsRes = new Vector2(Settings.GameWidth, Settings.GameHeight);
            }

            if (Settings.IS_FULLSCREEN)
                SetFullScreen(true);
            else
                SetStopFullScreen(true);
        }









        //public static float PixelRatio = 2;
        public static bool IS_MOUSE_VISIBLE = true;
        public static bool IS_FULLSCREEN = false;
        public static Color BACKGROUND_COLOR = Color.Black;
        public static Keys[] KeyUP = { Keys.Z, Keys.Up };
        public static Keys[] KeyDown = { Keys.S, Keys.Down };
        public static Keys[] KeyRight = { Keys.D, Keys.Right };
        public static Keys[] KeyLeft = { Keys.Q, Keys.Left };
        public static Keys[] KeySpeed = { Keys.LeftShift, Keys.RightShift };
        public static Keys[] bullet = { Keys.Space, Keys.RightAlt};
        
       // public static Keys[] KeyBomb = { Keys.C, Keys.RightAlt };

        public static string appDataFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string SaveFILE = "Save.xml";
        public static string SettingFILE = "Set.xml";
        //public static Save Save = new Save(1, "Rouge Zelda", appDataFilePath);
        public static CultureInfo culture = new CultureInfo("en-US");

       //public static void SaveSettings()
       // {
       //     Settings.Initialaize obj = Settings.Initialaize.GetSetting();
       //     Save.SaveSettings(obj, SettingFILE);
       // }
       // public static void LoadSetting()
       // {
       //     if (!File.Exists(Save.baseFolder + Settings.SettingFILE))
       //     { Settings.SaveSettings();
       //         return;
       //     }
       //     Settings.Initialaize obj= Save.LoadSetting(SettingFILE);
       //     Settings.Initialaize.SetSettings(obj);
       // }
        public static void Defult()
        {
            Settings.Initialaize obj = new Initialaize(/*2,*/ true, Keys.Z, Keys.S, Keys.D, Keys.Q, Keys.LeftShift, Keys.A, /*Keys.C,*/
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Save.xml", "Set.xml");

            Settings.Initialaize.SetSettings(obj);
        }
        public class Initialaize
        {
            #region Fields

            public float PixelRatio { get; set; }
            public bool IS_MOUSE_VISIBLE { get; set; }
            public Keys KeyUP { get; set; }
            public Keys KeyDown { get; set; }
            public Keys KeyRight { get; set; }
            public Keys KeyLeft { get; set; }
            public Keys KeySpeed { get; set; }
            public Keys bullet { get; set; }            
            public Keys KeyBomb { get; set; }
            public string appDataFilePath { get; set; }
            public string SaveFILE { get; set; }
            public string SettingFILE { get; set; }
            public Initialaize()
            {

            }
            #endregion
            public Initialaize(/*float PixelRatio,*/ bool IS_MOUSE_VISIBLE, Keys KeyUP, Keys KeyDown, Keys KeyRight, Keys KeyLeft, Keys KeySpeed, Keys bullet,/* Keys KeyBomb,*/ string appDataFilePath, string SaveFILE,string SettingFILE)
            {
                this.PixelRatio = PixelRatio;
                this.IS_MOUSE_VISIBLE = IS_MOUSE_VISIBLE;
                this.KeyUP = KeyUP;
                this.KeyDown = KeyDown;
                this.KeyRight = KeyRight;
                this.KeyLeft = KeyLeft;
                this.KeySpeed = KeySpeed;
                this.bullet = bullet;
                
                this.KeyBomb = KeyBomb;
                this.appDataFilePath = appDataFilePath;
                this.SaveFILE = SaveFILE;
                this.SettingFILE = SettingFILE;
            }
            public static void SetSettings(Initialaize initialaize)
            {
                //Settings.PixelRatio = initialaize.PixelRatio;
                Settings.IS_MOUSE_VISIBLE = initialaize.IS_MOUSE_VISIBLE;
                Settings.KeyUP[0] = initialaize.KeyUP;
                Settings.KeyDown[0] = initialaize.KeyDown;
                Settings.KeyRight[0] = initialaize.KeyRight;
                Settings.KeyLeft[0] = initialaize.KeyLeft;
                Settings.KeySpeed[0] = initialaize.KeySpeed;
                Settings.bullet[0] = initialaize.bullet;
                
                //Settings.KeyBomb[0] = initialaize.KeyBomb;
                Settings.SettingFILE = initialaize.SettingFILE;

                //if (Settings.appDataFilePath != initialaize.appDataFilePath || Settings.SaveFILE != initialaize.SaveFILE)
                //{
                //    Settings.appDataFilePath = initialaize.appDataFilePath;
                //    Settings.SaveFILE = initialaize.SaveFILE;
                //    Settings.Save = new Save(1, "Rouge Zelda", initialaize.appDataFilePath);
                //}
            }
            public static Initialaize GetSetting()
            {
                return new Initialaize(/*Settings.PixelRatio,*/ Settings.IS_MOUSE_VISIBLE,
                    Settings.KeyUP[0], Settings.KeyDown[0], Settings.KeyRight[0], Settings.KeyLeft[0],
                    Settings.KeySpeed[0], Settings.bullet[0], /*Settings.KeyBomb[0],*/
                    Settings.appDataFilePath, Settings.SaveFILE,Settings.SettingFILE);
            }
        }
    }
}
