using System;
using SadConsole;
using Microsoft.Xna.Framework;

namespace the_invincible_overlord.Tiles {
    class TileWall : TileBase {
        public TileWall(bool blocksMovement=true,
            bool blocksLOS=true) : base(Color.LightGray, Color.Transparent, '#', blocksMovement, blocksLOS) {
            Name = "Wall";
        }
    }
}
