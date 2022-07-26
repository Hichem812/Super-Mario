using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MyLibrary
{
    //delegate void Update();
    public class Input
    {
        public static KeyboardState oldKeyboard;
        public static MouseState oldMouse;
        public static KeyboardState Curentkeyboard;
        public static MouseState CurentMouse;
        //static Update update=new Update(Initialaze);
        public static void Initialaze()
        {
            //Keyboard.GetState(), Mouse.GetState()
            Curentkeyboard = Keyboard.GetState();
            CurentMouse = Mouse.GetState();
            Update();
            //Update2();
            //update = new Update(Update2);                 
        }

        public static bool IsKey(Keys[] keys)
        {
            foreach (Keys key in keys)
                if (oldKeyboard.IsKeyUp(key) && Curentkeyboard.IsKeyDown(key)) return true;

            return false;
        }
        public static bool IsKey(Keys key)
        {
            return oldKeyboard.IsKeyUp(key) && Curentkeyboard.IsKeyDown(key);
        }
        public static bool IsAnythingKey()
        {
            return oldKeyboard != Curentkeyboard;
        }
        public bool IsMouseClicked()
        {
            return oldMouse.LeftButton == ButtonState.Pressed && CurentMouse.LeftButton == ButtonState.Released;
        }



        //static void Update2()
        //{
        //    oldKeyboard = Curentkeyboard;
        //    oldMouse = CurentMouse;
        //    Curentkeyboard = Keyboard.GetState();
        //    CurentMouse = Mouse.GetState();
        //}
        public static void Update()
        {
            //update();
            oldKeyboard = Curentkeyboard;
            oldMouse = CurentMouse;
            Curentkeyboard = Keyboard.GetState();
            CurentMouse = Mouse.GetState();
        }
        //public static void Update(KeyboardState Curentkeyboard, MouseState CurentMouse)
        //{
        //    Input.oldKeyboard = Input.Curentkeyboard;
        //    Input.oldMouse = Input.CurentMouse;
        //    Input.Curentkeyboard = Curentkeyboard;
        //    Input.CurentMouse = CurentMouse;
        //}

        //public class Input
        //{
        //    #region FIELDS

        //    KeyboardState oldKeyboard;
        //    MouseState oldMouse;
        //    KeyboardState Curentkeyboard;
        //    MouseState CurentMouse;

        //    #endregion

        //    #region CONSTRUCTOR
        //    public Input(KeyboardState oldKeyboard, MouseState oldMouse, KeyboardState keyboard, MouseState mouse)
        //    {
        //        this.oldKeyboard = oldKeyboard;
        //        this.oldMouse = oldMouse;

        //        this.Curentkeyboard = keyboard;
        //        this.CurentMouse = mouse;
        //        //  this.Iskey = true;
        //    }
        //    #endregion

        //    #region METHODS

        //    public Keys GetKeyPressed()
        //    {
        //        Keys[] k = this.Curentkeyboard.GetPressedKeys();
        //        if (k.Length > 0) return k[0];
        //        else return Keys.None;
        //        //return this.Curentkeyboard.GetPressedKeys()[0];
        //    }
        public static bool IskeyDown(Keys[] keys)
        {
            foreach (Keys key in keys)
                if (Curentkeyboard.IsKeyDown(key)) return true;

            return false;
        }
        public static bool IskeyDown(Keys key)
        {
            return Curentkeyboard.IsKeyDown(key);
        }
        public static bool IskeyUp(Keys[] keys)
        {
            foreach (Keys key in keys)
                if (Curentkeyboard.IsKeyUp(key)) return true;

            return false;
        }
        public static bool IskeyUp(Keys key)
        {
            return Curentkeyboard.IsKeyUp(key);
        }

        //    internal bool IsKey(object keys)
        //    {
        //        throw new NotImplementedException();
        //    }


        //    //public void ActiveIsKey() { this.Iskey = true; }
        //    public bool IsKey(Keys key)
        //    {
        //        //if (this.oldKeyboard.IsKeyUp(key) && this.Curentkeyboard.IsKeyDown(key) && this.Iskey)
        //        //{
        //        //    this.Iskey = false;
        //        //    return true;
        //        //}
        //        //else return false;
        //        return this.oldKeyboard.IsKeyUp(key) && this.Curentkeyboard.IsKeyDown(key);
        //    }
        //    public bool IsAnythingKey()
        //    {
        //        return this.oldKeyboard != Curentkeyboard;
        //    }
        //    public bool IsLeftMouseDown()
        //    {
        //        return this.CurentMouse.LeftButton == ButtonState.Pressed;
        //    }

        //    public bool IsMouseClicked()
        //    {
        //        return this.oldMouse.LeftButton == ButtonState.Pressed && this.CurentMouse.LeftButton == ButtonState.Released;
        //    }

        //    public Point GetMousePosition()
        //    {
        //        //  return new Point(this.CurentMouse.X, this.CurentMouse.Y);
        //        return new Point(this.CurentMouse.Position.X / (int)Settings.PixelRatio, this.CurentMouse.Position.Y / (int)Settings.PixelRatio);
        //    }
        //    #endregion        
        //}
    }
}
