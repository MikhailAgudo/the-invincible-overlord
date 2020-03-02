using System;
using SadConsole;
using Microsoft.Xna.Framework;

namespace the_invincible_overlord.Tiles {
    public abstract class TileBase : Cell {
        // Movement and LOS Flags
        protected bool IsBlockingMove;
        protected bool IsBlockingLOS;

        // Tile's Name
        protected string Name;
        
        // Default Constructor
        public TileBase(Color foreground, 
            Color background,
            int glyph,
            bool blockingMove=false,
            bool blockingLOS=false,
            string name="") : base(foreground, background, glyph) {
            IsBlockingMove = blockingMove;
            IsBlockingLOS = blockingLOS;
            Name = name;
        }
    }
}
