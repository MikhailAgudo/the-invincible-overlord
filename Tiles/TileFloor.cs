using System;
using SadConsole;
using Microsoft.Xna.Framework;

namespace the_invincible_overlord.Tiles {
    class TileFloor : TileBase {
        public TileFloor(
            bool blocksMovement=false,
            bool blocksLOS=false) : base(Color.DarkGray, Color.Transparent, '.', blocksMovement, blocksLOS) {
            Name = "Floor";
        }
    }
}
