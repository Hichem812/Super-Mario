using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary

{
    public class Point_Byte
    {
        // Fields        
        public byte X;
        public byte Y;


        // Constructor
        /// <summary>
        /// Pare Defolt x=0 y=0
        /// </summary>
        /// <param name="FrameX"></param>
        /// <param name="FrameY"></param>
        public Point_Byte(byte FrameX = 0, byte FrameY = 0)
        {
            this.X = FrameX;
            this.Y = FrameY;
        }
        public Point_Byte(Point_Byte Point)
        {
            this.X = Point.X;
            this.Y = Point.Y;
        }
        public bool Equals(Point_Byte p)
        {
            return this.X == p.X && this.Y == p.Y;
        }
        public void Clone(Point_Byte Point)
        {
            this.X = Point.X;
            this.Y = Point.Y;
        }
    }
}
