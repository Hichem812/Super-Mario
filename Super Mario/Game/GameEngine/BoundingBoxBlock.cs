using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Mario
{
    internal class BoundingBoxBlock : BoundingBox
    {
        Block block;
        public BoundingBoxBlock(Rectangle bount, Block block) : base(bount)
        {
            this.block = block;
        }

        public override bool IsTouchingBottom(Rectangle Hitbox,PowerStateType type)
        {
            bool Touching = base.IsTouchingBottom(Hitbox, type);
            if (Touching) this.block.IsTouchingBottom(type);
            return Touching;
        }

    }
}
